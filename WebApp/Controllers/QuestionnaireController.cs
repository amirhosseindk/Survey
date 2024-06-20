using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.Models;

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
            var courses = _context.Courses.Where(c => c.ProfessorId.ToString() == _userManager.GetUserId(User)).ToList();
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

            var questionnaire = new Questionnaire
            {
                Title = data.Title,
                CourseId = int.Parse(data.Course),
                ProfessorId = _userManager.GetUserId(User),
                Questions = new List<Question>()
            };

            foreach (var question in data.Questions)
            {
                Console.WriteLine($"Processing question: {question.Title} of type {question.Type}");
                switch (question.Type)
                {
                    case "Text":
                        questionnaire.Questions.Add(new TextQuestion(Convert.ToInt16(question.Rank), question.Title)
                        {
                            Type = QuestionType.Text
                        });
                        break;
                    case "MultipleChoice":
                        questionnaire.Questions.Add(new MultipleChoiceQuestion(Convert.ToInt16(question.Rank), question.Title)
                        {
                            Type = QuestionType.MultipleChoice,
                            Options = question.Options.Select(o => new MultipleChoiceOption { OptionText = o }).ToList()
                        });
                        break;
                    case "Range":
                        questionnaire.Questions.Add(new RangeQuestion(Convert.ToInt16(question.Rank), question.Title)
                        {
                            Type = QuestionType.Range
                        });
                        break;
                    case "Degree":
                        questionnaire.Questions.Add(new DegreeQuestion(Convert.ToInt16(question.Rank), question.Title)
                        {
                            Type = QuestionType.Degree
                        });
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
            var results = await _context.Answers
                .Where(a => a.QuestionnaireId == id)
                .ToListAsync();

            if (!results.Any())
            {
                return NotFound();
            }

            return View(results);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}