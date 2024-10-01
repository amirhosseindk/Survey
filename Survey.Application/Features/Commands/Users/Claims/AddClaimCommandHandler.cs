using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.Users.Contracts;
using System.Security.Claims;

namespace Survey.Application.Features.Commands.Users.Claims
{
    public class AddClaimCommandHandler : IRequestHandler<AddClaimCommand, ResultDto<bool>>
    {
        private readonly IUserService _userService;

        public AddClaimCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ResultDto<bool>> Handle(AddClaimCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return false.ToResultDto();
            }

            var claim = new Claim(request.ClaimType, request.ClaimValue);
            var result = await _userService.AddClaimAsync(user, claim);
            return result.Succeeded.ToResultDto();
        }
    }
}