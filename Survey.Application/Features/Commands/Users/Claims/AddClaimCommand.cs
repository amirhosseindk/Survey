using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Users.Claims
{
    public class AddClaimCommand : BaseRequest<ResultDto<bool>>
    {
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}