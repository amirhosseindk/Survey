using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Commands.Universities.StudentClass
{
    public class RemoveStudentFromClassCommandHandler : IRequestHandler<RemoveStudentFromClassCommand, ResultDto<bool>>
    {
        private readonly IUniversityService _universityService;

        public RemoveStudentFromClassCommandHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<bool>> Handle(RemoveStudentFromClassCommand request, CancellationToken cancellationToken)
        {
            var result = await _universityService.RemoveStudentFromClassAsync(request.ClassId, request.StudentId);
            return result.ToResultDto();
        }
    }
}