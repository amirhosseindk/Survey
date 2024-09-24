using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Suvery.WebAPI.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}