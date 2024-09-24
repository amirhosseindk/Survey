namespace Survey.Application.Features.Commands.Users.SignUp
{
    public class SignUpCommand : BaseRequest<bool>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StudentNumber { get; set; }
        public bool IsProfessor { get; set; }
    }
}