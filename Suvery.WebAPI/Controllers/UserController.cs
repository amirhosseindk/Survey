using MediatR;
using Microsoft.AspNetCore.Mvc;
using Survey.Application.Dtos.Result;
using Survey.Application.Dtos.Users.Base;
using Survey.Application.Dtos.Users.Claim;
using Survey.Application.Dtos.Users.Email;
using Survey.Application.Dtos.Users.Password;
using Survey.Application.Dtos.Users.Roles;
using Survey.Application.Dtos.Users.SignIn;
using Survey.Application.Dtos.Users.SignUp;
using Survey.Application.Features.Commands.Users.Claims;
using Survey.Application.Features.Commands.Users.Delete;
using Survey.Application.Features.Commands.Users.Email;
using Survey.Application.Features.Commands.Users.Password;
using Survey.Application.Features.Commands.Users.Roles;
using Survey.Application.Features.Commands.Users.SignOut;
using Survey.Application.Features.Commands.Users.SignUp;
using Survey.Application.Features.Commands.Users.SignIn;
using Survey.Application.Features.Queries.Users.Email;
using Survey.Application.Features.Queries.Users.Id;
using Survey.Application.Features.Queries.Users.Roles;
using Survey.Application.Features.Queries.Users.Username;
using Suvery.WebAPI.Extensions;
using Suvery.WebAPI.Filters;

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
            var result = await _mediator.Send(new SignInCommand
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
            var result = await _mediator.Send(new SignInWithClaimsCommand
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

        [HttpPut]
        [Route("api/[controller]/ChangePassword")]
        public async Task<ResultDto<bool>> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var result = await _mediator.Send(new ChangePasswordCommand
            {
                UserId = dto.UserId,
                CurrentPassword = dto.CurrentPassword,
                NewPassword = dto.NewPassword
            });

            return result;
        }

        [HttpPut]
        [Route("api/[controller]/AddClaim")]
        public async Task<ResultDto<bool>> AddClaim([FromBody] AddClaimDto dto)
        {
            var result = await _mediator.Send(new AddClaimCommand
            {
                UserId = dto.UserId,
                ClaimType = dto.ClaimType,
                ClaimValue = dto.ClaimValue
            });

            return result;
        }

        [HttpPut]
        [Route("api/[controller]/AddToRole")]
        public async Task<ResultDto<bool>> AddToRole([FromBody] AddToRoleDto dto)
        {
            var result = await _mediator.Send(new AddToRoleCommand
            {
                UserId = dto.UserId,
                Role = dto.Role
            });

            return result;
        }

        [HttpPut]
        [Route("api/[controller]/ChangeEmail")]
        public async Task<ResultDto<bool>> ChangeEmail([FromBody] ChangeEmailDto dto)
        {
            var result = await _mediator.Send(new ChangeEmailCommand
            {
                UserId = dto.UserId,
                NewEmail = dto.NewEmail,
                Token = dto.Token
            });

            return result;
        }

        [HttpDelete]
        [Route("api/[controller]/RemoveClaim")]
        public async Task<ResultDto<bool>> RemoveClaim([FromBody] RemoveClaimDto dto)
        {
            var result = await _mediator.Send(new RemoveClaimCommand
            {
                UserId = dto.UserId,
                ClaimType = dto.ClaimType,
                ClaimValue = dto.ClaimValue
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/GetRoles/{userId}")]
        public async Task<ResultDto<IList<string>>> GetRoles(string userId)
        {
            var result = await _mediator.Send(new GetRolesQuery 
            {
                UserId = userId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/FindByEmail/{email}")]
        public async Task<ResultDto<UserDto>> FindByEmail(string email)
        {
            var result = await _mediator.Send(new FindByEmailQuery 
            {
                Email = email
            });
            return result;
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteUser")]
        public async Task<ResultDto<bool>> DeleteUser(string userId)
        {
            var result = await _mediator.Send(new DeleteUserCommand
            {
                UserId = userId
            });

            return result;
        }

        [HttpGet]
        [Route("api/[controller]/FindByUsername/{userName}")]
        public async Task<ResultDto<UserDto>> FindByUsername(string userName)
        {
            var result = await _mediator.Send(new FindByUsernameQuery 
            {
                Username = userName
            });
            return result;
        }

        [HttpGet]
        [Route("api/[controller]/FindByUserId/{userId}")]
        public async Task<ResultDto<UserDto>> FindByUserId(string userId)
        {
            var result = await _mediator.Send(new FindByUserIdQuery
            {
                UserId = userId
            });
            return result;
        }
    }
}