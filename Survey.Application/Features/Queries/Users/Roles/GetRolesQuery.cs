using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Users.Roles
{
    public class GetRolesQuery : BaseRequest<ResultDto<IList<string>>>
    {
        public string UserId { get; set; }
    }
}