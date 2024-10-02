using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Users.Email
{
    public class ChangeEmailCommand : BaseRequest<ResultDto<bool>>
    {
        public string UserId { get; set; }
        public string NewEmail { get; set; }
        public string Token { get; set; }
    }
}