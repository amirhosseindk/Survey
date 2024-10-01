using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Users.Contracts;

namespace Survey.Application.Features.Commands.Users.Password
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ResultDto<bool>>
    {
        private readonly IUserService _userService;

        public ChangePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return false.ToResultDto();
            }

            var result = await _userService.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            return result.Succeeded.ToResultDto();
        }
    }
}