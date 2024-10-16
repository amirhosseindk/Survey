using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survey.Application.Features.Queries.Questionnaires.GetQuestionnairesByStudentId;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "Student")]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserId();

            var questionnaires = await _mediator.Send(new GetQuestionnairesByStudentIdQuery { StudentId = userId});

            // todo course name va class name byd begirim

            return View(questionnaires.Result);
        }
    }
}