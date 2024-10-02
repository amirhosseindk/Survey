using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Users.Contracts;

namespace Survey.Application.Features.Commands.Users.Email
{
    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, ResultDto<bool>>
    {
        private readonly IUserService _userService;

        public ChangeEmailCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<bool>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return false.ToResultDto();
            }

            var result = await _userService.ChangeEmailAsync(user, request.NewEmail, request.Token);
            return result.Succeeded.ToResultDto();
        }
    }
}