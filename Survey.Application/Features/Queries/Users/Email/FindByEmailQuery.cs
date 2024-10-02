using Survey.Application.Dtos.Result;
using Survey.Application.Dtos.Users.Base;

namespace Survey.Application.Features.Queries.Users.Email
{
    public class FindByEmailQuery : BaseRequest<ResultDto<UserDto>>
    {
        public string Email { get; set; }
    }
}