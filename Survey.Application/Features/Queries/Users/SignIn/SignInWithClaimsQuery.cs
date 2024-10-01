using Survey.Application.Dtos.Result;
using Survey.Users.Models;

namespace Survey.Application.Features.Queries.Users.SignIn
{
    public class SignInWithClaimsQuery : BaseRequest<ResultDto<bool>>
    {
        public User User { get; set; }
    }
}