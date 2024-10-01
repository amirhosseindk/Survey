using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Users.Roles
{
    public class AddToRoleCommand : BaseRequest<ResultDto<bool>>
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}