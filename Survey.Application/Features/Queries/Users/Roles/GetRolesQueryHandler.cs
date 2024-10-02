using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Users.Contracts;

namespace Survey.Application.Features.Queries.Users.Roles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, ResultDto<IList<string>>>
    {
        private readonly IUserService _userService;

        public GetRolesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<IList<string>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new ResultDto<IList<string>> { Succeeded = false, Result = null };
            }

            var roles = await _userService.GetRolesAsync(user);
            return new ResultDto<IList<string>> { Succeeded = true, Result = roles };
        }
    }
}