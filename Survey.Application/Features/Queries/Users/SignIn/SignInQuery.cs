using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Users.SignIn
{
    public class SignInQuery : BaseRequest<ResultDto<bool>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}