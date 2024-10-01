using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Users.Contracts;

namespace Survey.Application.Features.Queries.Users.SignIn
{
    public class SignInWithClaimsQueryHandler : IRequestHandler<SignInWithClaimsQuery, ResultDto<bool>>
    {
        private readonly IUserService _userService;

        public SignInWithClaimsQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<bool>> Handle(SignInWithClaimsQuery request, CancellationToken cancellationToken)
        {
            var result = await _userService.SignInAsync(request.User);
            return result.ToResultDto();
        }
    }
}