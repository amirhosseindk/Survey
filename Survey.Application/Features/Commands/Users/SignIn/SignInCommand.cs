using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Users.SignIn
{
    public class SignInCommand : BaseRequest<ResultDto<bool>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}