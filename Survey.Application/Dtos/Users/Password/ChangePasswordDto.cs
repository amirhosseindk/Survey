namespace Survey.Application.Dtos.Users.Password
{
    public class ChangePasswordDto
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}