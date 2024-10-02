namespace Survey.Application.Dtos.Users.Email
{
    public class ChangeEmailDto
    {
        public string UserId { get; set; }
        public string NewEmail { get; set; }
        public string Token { get; set; }
    }
}