using MediatR;
using Survey.Users.Contracts;

namespace Survey.Application.Features.Queries.Users.SignIn
{
    public class SignInQueryHandler : IRequestHandler<SignInQuery, bool>
    {
        private readonly IUserService _userService;

        public SignInQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            var result = await _userService.SignInAsync(request.Username, request.Password);
            return result;
        }
    }
}