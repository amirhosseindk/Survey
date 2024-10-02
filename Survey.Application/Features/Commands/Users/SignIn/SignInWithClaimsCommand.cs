using Survey.Application.Dtos.Result;
using Survey.Application.Dtos.Users.Base;

namespace Survey.Application.Features.Commands.Users.SignIn
{
    public class SignInWithClaimsCommand : BaseRequest<ResultDto<bool>>
    {
        public UserDto User { get; set; }
    }
}