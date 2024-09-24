using MediatR;
using Microsoft.AspNetCore.Mvc;
using Survey.Application.Dtos.Users.Login;
using Survey.Application.Dtos.Users.SignUp;
using Survey.Application.Features.Commands.Users.SignUp;
using Survey.Application.Features.Queries.Users.Login;

namespace Suvery.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<bool> Login([FromBody] LoginDto dto)
        {
            var result = await _mediator.Send(new LoginQuery
            {
                Username = dto.Username,
                Password = dto.Password
            });

            return result;
        }

        [HttpPost]
        public async Task<bool> SignUp([FromBody] CreateUserDto dto)
        {
            var result = await _mediator.Send(new SignUpCommand
            {
                Username = dto.Username,
                Password = dto.Password,
                StudentNumber = dto.StudentNumber,
                Email = dto.Email,
                IsProfessor = dto.IsProfessor
            });

            return result;
        }
    }
}