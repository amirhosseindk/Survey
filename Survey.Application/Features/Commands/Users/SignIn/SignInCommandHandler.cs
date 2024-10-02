using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Users.Contracts;

namespace Survey.Application.Features.Commands.Users.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, ResultDto<bool>>
    {
        private readonly IUserService _userService;

        public SignInCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<bool>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.SignInAsync(request.Username, request.Password);
            return result.ToResultDto();
        }
    }
}