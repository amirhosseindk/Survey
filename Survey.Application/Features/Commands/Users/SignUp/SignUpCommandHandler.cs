using MediatR;
using Survey.Users.Contracts;
using Survey.Users.Models;

namespace Survey.Application.Features.Commands.Users.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, bool>
    {
        private readonly IUserService _userService;

        public SignUpCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                StudentNumber = request.StudentNumber,
                IsProfessor = request.IsProfessor
            };

            var result = await _userService.CreateAsync(user,request.Password);

            return result.Succeeded;
        }
    }
}