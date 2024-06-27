using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class QuestionnaireController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public QuestionnaireController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Professor")]
        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(User);
            var courses = _context.Courses
                .Where(c => c.ProfessorId == userId)
                .Include(c => c.Classes)
                .ToList();
            ViewBag.Courses = courses;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionnaireDto data)
        {
            if (data == null)
            {
                Console.WriteLine("Received null data.");
                return BadRequest("Data is null.");
            }

            data.Questions.RemoveAll(d => d.Rank == null);

            var title = data.Title;
            var classId = int.Parse(data.Class);
            var professorId = _userManager.GetUserId(User);
            var questions = new List<Question>();

            var questionnaire = new Questionnaire
            {
                Title = title,
                ClassId = classId,
                ProfessorId = professorId,
                Questions = questions
            };

            foreach (var question in data.Questions)
            {
                switch (question.Type)
                {
                    case "Text":
                        questionnaire.Questions.Add(new TextQuestion(Convert.ToInt16(question.Rank), question.Title) { Type = QuestionType.Text });
                        break;
                    case "MultipleChoice":
                        questionnaire.Questions.Add(new MultipleChoiceQuestion(Convert.ToInt16(question.Rank), question.Title)
                        {
                            Type = QuestionType.MultipleChoice,
                            Options = question.Options.Select(o => new MultipleChoiceOption { OptionText = o }).ToList()
                        });
                        break;
                    case "Range":
                        questionnaire.Questions.Add(new RangeQuestion(Convert.ToInt16(question.Rank), question.Title) { Type = QuestionType.Range });
                        break;
                    case "Degree":
                        questionnaire.Questions.Add(new DegreeQuestion(Convert.ToInt16(question.Rank), question.Title) { Type = QuestionType.Degree });
                        break;
                }
            }

            _context.Questionnaires.Add(questionnaire);
            await _context.SaveChangesAsync();
            return Ok(new { questionnaireId = questionnaire.Id });
        }   

        public async Task<IActionResult> Fill(int id)
        {
            var questionnaire = await _context.Questionnaires
                .Include(q => q.Questions)
                .ThenInclude(q => (q as MultipleChoiceQuestion).Options)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (questionnaire == null)
            {
                return NotFound();
            }
            return View(questionnaire);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAnswers([FromBody] List<AnswerDto> answers)
        {
            if (answers == null)
            {
                Console.WriteLine("Received null answers.");
                return BadRequest("Answers are null.");
            }

            foreach (var answer in answers)
            {
                _context.Answers.Add(new Answer
                {
                    QuestionnaireId = answer.QuestionnaireId,
                    QuestionId = answer.QuestionId,
                    AnswerText = answer.AnswerText,
                    AnswerOptionId = answer.AnswerOptionId,
                    StudentId = _userManager.GetUserId(User)
                });
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> Results(int id)
        {
            var questionnaire = await _context.Questionnaires
                .Include(q => q.Class)
                .ThenInclude(c => c.Students)
                .Include(q => q.Questions)
                .ThenInclude(q => ((MultipleChoiceQuestion)q).Options)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            var totalStudents = questionnaire.Class.Students.Count;
            var answeredStudents = await _context.Answers
                .Where(a => a.QuestionnaireId == id)
                .Select(a => a.StudentId)
                .Distinct()
                .CountAsync();

            var multipleChoiceResults = await _context.Answers
                .Where(a => a.QuestionnaireId == id && a.AnswerOptionId != null)
                .GroupBy(a => new { a.QuestionId, a.AnswerOptionId })
                .Select(g => new MultipleChoiceResult { QuestionId = g.Key.QuestionId, AnswerOptionId = g.Key.AnswerOptionId, Count = g.Count() })
                .ToListAsync();

            var textQuestionResults = await _context.Answers
                .Where(a => a.QuestionnaireId == id && a.AnswerText != null)
                .GroupBy(a => a.QuestionId)
                .Select(g => new TextQuestionResult { QuestionId = g.Key, Count = g.Count() })
                .ToListAsync();

            return View(new SurveyResultsViewModel
            {
                Questionnaire = questionnaire,
                MultipleChoiceResults = multipleChoiceResults,
                TextQuestionResults = textQuestionResults,
                TotalStudents = totalStudents,
                AnsweredStudents = answeredStudents
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}