using Survey.Application.Dtos.Result;
using Survey.Application.Dtos.Users.Base;

namespace Survey.Application.Features.Queries.Users.Username
{
    public class FindByUsernameQuery : BaseRequest<ResultDto<UserDto>>
    {
        public string Username { get; set; }
    }
}