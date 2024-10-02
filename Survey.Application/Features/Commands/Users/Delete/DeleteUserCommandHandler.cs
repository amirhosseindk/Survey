using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Users.Contracts;

namespace Survey.Application.Features.Commands.Users.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResultDto<bool>>
    {
        private readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return false.ToResultDto();
            }

            var result = await _userService.DeleteAsync(user);
            return result.Succeeded.ToResultDto();
        }
    }
}