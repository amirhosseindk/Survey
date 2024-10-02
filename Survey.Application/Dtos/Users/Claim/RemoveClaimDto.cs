namespace Survey.Application.Dtos.Users.Claim
{
    public class RemoveClaimDto
    {
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}