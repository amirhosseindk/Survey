using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Users.Contracts;

namespace Survey.Application.Features.Commands.Users.SignOut
{
    public class SignOutCommandHandler : IRequestHandler<SignOutCommand, ResultDto<bool>>
    {
        private readonly IUserService _userService;

        public SignOutCommandHandler(IUserService userService) 
        {
            _userService = userService;
        }

        public async Task<ResultDto<bool>> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.SignOutAsync();
            return result.ToResultDto();
        }
    }
}