using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Commands.Universities.StudentClass
{
    public class AddStudentToClassCommandHandler : IRequestHandler<AddStudentToClassCommand, ResultDto<bool>>
    {
        private readonly IUniversityService _universityService;

        public AddStudentToClassCommandHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<bool>> Handle(AddStudentToClassCommand request, CancellationToken cancellationToken)
        {
            var result = await _universityService.AddStudentToClassAsync(request.ClassId, request.StudentId);
            return result.ToResultDto();
        }
    }
}
