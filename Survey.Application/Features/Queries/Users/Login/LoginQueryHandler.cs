using MediatR;
using Survey.Users.Contracts;

namespace Survey.Application.Features.Queries.Users.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, bool>
    {
        private readonly IUserService _userService;

        public LoginQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var result = await _userService.LoginAsync(request.Username, request.Password);
            return result;
        }
    }
}