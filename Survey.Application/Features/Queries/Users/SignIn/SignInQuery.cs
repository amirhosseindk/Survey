namespace Survey.Application.Features.Queries.Users.SignIn
{
    public class SignInQuery : BaseRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}