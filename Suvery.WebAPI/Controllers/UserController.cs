using MediatR;
using Microsoft.AspNetCore.Mvc;
using Survey.Application.Dtos.Result;
using Survey.Application.Dtos.Users.Claim;
using Survey.Application.Dtos.Users.Login;
using Survey.Application.Dtos.Users.Password;
using Survey.Application.Dtos.Users.Roles;
using Survey.Application.Dtos.Users.SignUp;
using Survey.Application.Features.Commands.Users.Claims;
using Survey.Application.Features.Commands.Users.Password;
using Survey.Application.Features.Commands.Users.Roles;
using Survey.Application.Features.Commands.Users.SignOut;
using Survey.Application.Features.Commands.Users.SignUp;
using Survey.Application.Features.Queries.Users.SignIn;
using Suvery.WebAPI.Extensions;

namespace Suvery.WebAPI.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("api/[controller]/SignIn")]
        public async Task<ResultDto<bool>> SignIn([FromBody] SignInDto dto)
        {
            var result = await _mediator.Send(new SignInQuery
            {
                Username = dto.Username,
                Password = dto.Password
            });

            return result;
        }

        [HttpPost]
        [Route("api/[controller]/SignInithClaims")]
        public async Task<ResultDto<bool>> SignInWithClaims()
        {
            var result = await _mediator.Send(new SignInWithClaimsQuery
            {
                User = User.ToUserModel()
            });

            return result;
        }

        [HttpPost]
        [Route("api/[controller]/SignUp")]
        public async Task<ResultDto<bool>> SignUp([FromBody] CreateUserDto dto)
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

        [HttpPost]
        [Route("api/[controller]/SignOut")]
        public async Task<ResultDto<bool>> SignOut()
        {
            var result = await _mediator.Send(new SignOutCommand());

            return result;
        }

        [HttpPost]
        [Route("api/[controller]/ChangePassword")]
        public async Task<ResultDto<bool>> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var result = await _mediator.Send(new ChangePasswordCommand
            {
                UserId = User.GetUserId(),
                CurrentPassword = dto.CurrentPassword,
                NewPassword = dto.NewPassword
            });

            return result;
        }

        [HttpPost]
        [Route("api/[controller]/AddClaim")]
        public async Task<ResultDto<bool>> AddClaim([FromBody] AddClaimDto dto)
        {
            var result = await _mediator.Send(new AddClaimCommand
            {
                UserId = User.GetUserId(),
                ClaimType = dto.ClaimType,
                ClaimValue = dto.ClaimValue
            });

            return result;
        }

        [HttpPost]
        [Route("api/[controller]/AddToRole")]
        public async Task<ResultDto<bool>> AddToRole([FromBody] AddToRoleDto dto)
        {
            var result = await _mediator.Send(new AddToRoleCommand
            {
                UserId = User.GetUserId(),
                Role = dto.Role
            });

            return result;
        }
    }
}