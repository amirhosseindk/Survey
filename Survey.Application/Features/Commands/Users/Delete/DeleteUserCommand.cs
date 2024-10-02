using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Users.Delete
{
    public class DeleteUserCommand : BaseRequest<ResultDto<bool>>
    {
        public string UserId { get; set; }
    }
}