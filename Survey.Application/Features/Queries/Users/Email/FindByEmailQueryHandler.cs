using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Dtos.Users.Base;
using Survey.Users.Contracts;

namespace Survey.Application.Features.Queries.Users.Email
{
    public class FindByEmailQueryHandler : IRequestHandler<FindByEmailQuery, ResultDto<UserDto>>
    {
        private readonly IUserService _userService;

        public FindByEmailQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<UserDto>> Handle(FindByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ResultDto<UserDto> { Succeeded = false, Result = null };
            }

            var userDto = new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                StudentNumber = user.StudentNumber,
                IsProfessor = user.IsProfessor
            };

            return new ResultDto<UserDto> { Succeeded = true, Result = userDto };
        }
    }
}