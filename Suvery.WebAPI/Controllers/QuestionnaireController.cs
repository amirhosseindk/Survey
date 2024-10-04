using MediatR;
using Microsoft.AspNetCore.Mvc;
using Suvery.WebAPI.Filters;

namespace Suvery.WebAPI.Controllers
{
    [ApiController]
    [ApiKey]
    public class QuestionnaireController : BaseController
    {
        public QuestionnaireController(IMediator mediator) : base(mediator)
        {
        }
    }
}
