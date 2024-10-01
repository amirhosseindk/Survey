using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Users.Contracts;
using Survey.Users.Models;

namespace Survey.Application.Features.Commands.Users.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, ResultDto<bool>>
    {
        private readonly IUserService _userService;

        public SignUpCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<bool>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                StudentNumber = request.StudentNumber,
                IsProfessor = request.IsProfessor
            };

            var result = await _userService.CreateAsync(user,request.Password);

            return result.Succeeded.ToResultDto();
        }
    }
}