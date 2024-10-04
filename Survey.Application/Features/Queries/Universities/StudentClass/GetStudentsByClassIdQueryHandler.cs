using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.StudentClass
{
    public class GetStudentsByClassIdQueryHandler : IRequestHandler<GetStudentsByClassIdQuery, ResultDto<Dictionary<string, string>>>
    {
        private readonly IUniversityService _universityService;

        public GetStudentsByClassIdQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<Dictionary<string, string>>> Handle(GetStudentsByClassIdQuery request, CancellationToken cancellationToken)
        {
            var students = await _universityService.GetStudentsByClassIdAsync(request.ClassId);
            return students.ToResultDto();
        }
    }
}