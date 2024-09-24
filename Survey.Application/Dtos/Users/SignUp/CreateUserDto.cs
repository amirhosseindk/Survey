namespace Survey.Application.Dtos.Users.SignUp
{
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StudentNumber { get; set; }
        public bool IsProfessor { get; set; }
    }
}