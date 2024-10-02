using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Users.Contracts;
using Survey.Users.Models;

namespace Survey.Application.Features.Commands.Users.SignIn
{
    public class SignInWithClaimsCommandHandler : IRequestHandler<SignInWithClaimsCommand, ResultDto<bool>>
    {
        private readonly IUserService _userService;

        public SignInWithClaimsCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<bool>> Handle(SignInWithClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.SignInAsync(new User
            {
                Id = request.User.UserId,
                Email = request.User.Email,
                StudentNumber = request.User.StudentNumber,
                IsProfessor = request.User.IsProfessor,
                UserName = request.User.UserName
            });
            return result.ToResultDto();
        }
    }
}