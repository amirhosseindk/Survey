using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Users.Password
{
    public class ChangePasswordCommand : BaseRequest<ResultDto<bool>>
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}