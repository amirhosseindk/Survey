using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Commands.Universities.Classes
{
    public class DeleteClassCommandHandler : IRequestHandler<DeleteClassCommand, ResultDto<bool>>
    {
        private readonly IUniversityService _universityService;

        public DeleteClassCommandHandler(IUniversityService universityService) 
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<bool>> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            var result = await _universityService.DeleteClassAsync(request.ClassId);
            return result.ToResultDto();
        }
    }
}