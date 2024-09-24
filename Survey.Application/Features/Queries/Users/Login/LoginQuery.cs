namespace Survey.Application.Features.Queries.Users.Login
{
    public class LoginQuery : BaseRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}