using MediatR;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.StudentClass
{
    public class GetStudentsByClassNameQueryHandler : IRequestHandler<GetStudentsByClassNameQuery, ResultDto<Dictionary<string, string>>>
    {
        private readonly IUniversityService _universityService;

        public GetStudentsByClassNameQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<Dictionary<string, string>>> Handle(GetStudentsByClassNameQuery request, CancellationToken cancellationToken)
        {
            var students = await _universityService.GetStudentsByClassNameAsync(request.ClassName);
            return students.ToResultDto();
        }
    }
}