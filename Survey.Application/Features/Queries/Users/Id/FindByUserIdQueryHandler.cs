using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Dtos.Users.Base;
using Survey.Users.Contracts;

namespace Survey.Application.Features.Queries.Users.Id
{
    public class FindByUserIdQueryHandler : IRequestHandler<FindByUserIdQuery, ResultDto<UserDto>>
    {
        private readonly IUserService _userService;

        public FindByUserIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<UserDto>> Handle(FindByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId);
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

            return new ResultDto<UserDto> { Succeeded = false, Result = userDto };
        }
    }
}