using Survey.Application.Dtos.Result;
using Survey.Application.Dtos.Users.Base;

namespace Survey.Application.Features.Queries.Users.Id
{
    public class FindByUserIdQuery : BaseRequest<ResultDto<UserDto>>
    {
        public string UserId { get; set; }
    }
}