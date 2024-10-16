using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Survey.Application.Features.Commands.Universities.Classes;
using Survey.Application.Features.Commands.Universities.Courses;
using Survey.Application.Features.Queries.Questionnaires.GetQuestionnaireByProfessorId;
using Survey.Application.Features.Queries.Universities.Courses;
using WebApp.Extensions;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "Professor")]
    public class ProfessorController : Controller
    {
        private readonly IMediator _mediator;
        
        public ProfessorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserId();
            var courses = await _mediator.Send(new GetAllCoursesByProfessorIdQuery { ProfessorId = userId });
            var questionnaires = await _mediator.Send(new GetQuestionnaireByProfessorIdQuery { Id = userId });
            ViewBag.Courses = courses.Result;
            return View(questionnaires.Result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(string courseName)
        {
            var userId = User.GetUserId();
            await _mediator.Send(new CreateCourseCommand { Name = courseName, ProfessorId = userId });
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CreateClass()
        {
            var userId = User.GetUserId();
            var courses = await _mediator.Send(new GetAllCoursesByProfessorIdQuery { ProfessorId = userId });
            ViewBag.Courses = courses.Result;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass(string className, int courseId)
        {
            await _mediator.Send(new CreateClassCommand { Name = className, CourseId = courseId });
            return RedirectToAction("Index");
        }
    }
}