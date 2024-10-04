using MediatR;
using Survey.Application.Dtos.Classes;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.StudentClass
{
    public class GetClassWithStudentsByNameQueryHandler : IRequestHandler<GetClassWithStudentsByNameQuery, ResultDto<ClassWithStudentsDto>>
    {
        private readonly IUniversityService _universityService;

        public GetClassWithStudentsByNameQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<ClassWithStudentsDto>> Handle(GetClassWithStudentsByNameQuery request, CancellationToken cancellationToken)
        {
            var @class = await _universityService.GetClassWithStudentsByNameAsync(request.ClassName);

            var result = new ClassWithStudentsDto
            {
                Id = @class.Id,
                Name = @class.Name,
                CourseId = @class.Course.Id,
                Students = @class.Students
            };

            return result.ToResultDto();
        }
    }
}