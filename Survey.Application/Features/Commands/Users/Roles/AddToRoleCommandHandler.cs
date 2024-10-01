using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Users.Contracts;

namespace Survey.Application.Features.Commands.Users.Roles
{
    public class AddToRoleCommandHandler : IRequestHandler<AddToRoleCommand, ResultDto<bool>>
    {
        private readonly IUserService _userService;

        public AddToRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<bool>> Handle(AddToRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return false.ToResultDto();
            }

            var result = await _userService.AddToRoleAsync(user, request.Role);
            return result.Succeeded.ToResultDto();
        }
    }
}