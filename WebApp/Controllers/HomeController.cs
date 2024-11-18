using MediatR;
using Microsoft.AspNetCore.Mvc;
using Survey.Application.Features.Queries.Questionnaires.GetQuestionnairesByStudentId;
using WebApp.Extensions;

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
            var questionnaires = await _mediator.Send(new GetQuestionnairesByStudentIdQuery { StudentId = User.GetUserId()});
            return View(questionnaires.Result);
        }
    }
}