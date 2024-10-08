using MediatR;
using Microsoft.AspNetCore.Mvc;
using Suvery.WebAPI.Filters;

namespace Suvery.WebAPI.Controllers
{
    [ApiController]
    [ApiKey]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}