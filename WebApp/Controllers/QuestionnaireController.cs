using MediatR;
using Microsoft.AspNetCore.Mvc;
using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Features.Commands.Answer;
using Survey.Application.Features.Commands.Questionnaires.CreateQuestionnaire;
using Survey.Application.Features.Queries.Answers.GetAnswersOfQuestionnaire;
using Survey.Application.Features.Queries.Answers.GetAnswersOfStudent;
using Survey.Application.Features.Queries.Questionnaires.GetQuestionnaireById;
using Survey.Application.Features.Queries.Universities.ClassCourse;
using Survey.Application.Features.Queries.Universities.Classes;
using Survey.Application.Features.Queries.Universities.Courses;
using Survey.Application.Features.Queries.Universities.StudentClass;
using WebApp.Extensions;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class QuestionnaireController : Controller
    {
        private readonly IMediator _mediator;
        private bool _updateAnswers;

        public QuestionnaireController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> CreateAsync()
        {
            var userId = User.GetUserId();
            var coursesOfProfessor = await _mediator.Send(new GetAllCoursesByProfessorIdQuery { ProfessorId = userId });

            var courseTasks = coursesOfProfessor.Result.Select(async course =>
            {
                var res = await _mediator.Send(new GetCourseWithClassesByIdQuery { CourseId = course.Id });
                return res.Result;
            });

            var coursesWithClasses = await Task.WhenAll(courseTasks);

            ViewBag.Courses = coursesWithClasses;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateQuestionnaireDto data)
        {
            if (data == null)
            {
                Console.WriteLine("Received null data.");
                return BadRequest("Data is null.");
            }
            
            var result = await _mediator.Send(new CreateQuestionnaireCommand 
            {
                Title = data.Title,
                ClassId = data.ClassId,
                ProffesorId = User.GetUserId(),
                Questions = data.Questions
            });

            return Ok(new { questionnaireId = result.Result });
        }

        public async Task<IActionResult> Fill(int id)
        {
            var questionnaire = await _mediator.Send(new GetQuestionnaireByIdQuery { Id = id });
            if (questionnaire == null)
            {
                return NotFound();
            }
            var answers = await _mediator.Send(new GetAnswersOfStudentQuery { QuestionnaireId = id, StudentId = User.GetUserId() });
            if (answers.Result.Count() != 0)
            {
                _updateAnswers = true;
                ViewBag.Answers = answers.Result;
                TempData["UpdateAnswers"] = true;
            }

            ViewBag.IsAnswered = _updateAnswers;
            ViewBag.QuestionnaireId = id;
            return View(questionnaire.Result);
        }

        [HttpPost]
        [Route("[controller]/SubmitAnswers/{id}")]
        public async Task<IActionResult> SubmitAnswers(int id, [FromBody] WebApp.Models.AnswerDto answersDto)
        {
            if (answersDto == null || (answersDto.CreateAnswers == null && answersDto.UpdateAnswers == null))
            {
                Console.WriteLine("Received null answers.");
                return BadRequest("Answers are null.");
            }

            _updateAnswers = TempData.ContainsKey("UpdateAnswers") && (bool)TempData["UpdateAnswers"];

            if (!_updateAnswers)
            {
                var result = await _mediator.Send(new CreateAnswerCommand { QuestionnaireId = id, Answers = answersDto.CreateAnswers });
                if (result.Result)
                    return Ok();
                else
                    return NotFound();
            }
            else
            {
                var result = await _mediator.Send(new UpdateAnswerCommand { QuestionnaireId = id, Answers = answersDto.UpdateAnswers });
                if (result.Result)
                    return Ok();
                else
                    return NotFound();
            }
        }

        //[Authorize(Roles = "Professor")]
        public async Task<IActionResult> Results(int id)
        {
            var questionnaire = await _mediator.Send(new GetQuestionnaireByIdQuery { Id = id });
            if (questionnaire == null)
            {
                return NotFound();
            }

            var @class = await _mediator.Send(new GetClassByIdQuery { ClassId = questionnaire.Result.ClassId });
            var students = await _mediator.Send(new GetStudentsByClassIdQuery { ClassId = questionnaire.Result.ClassId });

            var totalStudents = students.Result.Count;

            var answersResult = await _mediator.Send(new GetAnswersOfQuestionnaireQuery { QuestionnaireId = id });

            var answeredStudents = answersResult.Result
                .Select(a => a.StudentId)
                .Distinct()
                .Count();

            var multipleChoiceResults = answersResult.Result
                .Where(a => a.Type == 1)
                .GroupBy(a => new { a.QuestionId, a.AnswerOptionId })
                .Select(g => new MultipleChoiceResult
                {
                    QuestionId = g.Key.QuestionId,
                    AnswerOption = g.Key.AnswerOptionId ?? 0,
                    Count = g.Count()
                })
                .ToList();

            var textQuestionResults = answersResult.Result
                .Where(a => a.Type == 0 && !string.IsNullOrEmpty(a.AnswerText))
                .GroupBy(a => a.QuestionId)
                .Select(g => new TextQuestionResult
                {
                    QuestionId = g.Key,
                    Count = g.Count(),
                    Answers = g.Select(a => a.AnswerText).ToList()
                })
                .ToList();

            var rangeQuestionAverages = answersResult.Result
                .Where(a => a.Type == 2)
                .GroupBy(a => a.QuestionId)
                .Select(g => new RangeQuestionResult
                {
                    QuestionId = g.Key,
                    Average = g.Average(a => a.AnswerValue ?? 0)
                })
                .ToList();

            var degreeQuestionAverages = answersResult.Result
                .Where(a => a.Type == 3)
                .GroupBy(a => a.QuestionId)
                .Select(g => new DegreeQuestionResult
                {
                    QuestionId = g.Key,
                    Average = g.Average(a => a.AnswerValue ?? 0)
                })
                .ToList();

            return View(new SurveyResultsViewModel
            {
                Questionnaire = questionnaire.Result,
                MultipleChoiceResults = multipleChoiceResults,
                TextQuestionResults = textQuestionResults,
                TotalStudents = totalStudents,
                AnsweredStudents = answeredStudents,
                RangeQuestionResults = rangeQuestionAverages,
                DegreeQuestionResults = degreeQuestionAverages
            });
        }
    }
}