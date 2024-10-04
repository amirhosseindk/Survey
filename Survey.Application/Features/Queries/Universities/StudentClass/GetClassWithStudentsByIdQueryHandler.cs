using MediatR;
using Survey.Application.Dtos.Classes;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.StudentClass
{
    public class GetClassWithStudentsByIdQueryHandler : IRequestHandler<GetClassWithStudentsByIdQuery, ResultDto<ClassWithStudentsDto>>
    {
        private readonly IUniversityService _universityService;

        public GetClassWithStudentsByIdQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<ClassWithStudentsDto>> Handle(GetClassWithStudentsByIdQuery request, CancellationToken cancellationToken)
        {
            var @class = await _universityService.GetClassWithStudentsByIdAsync(request.ClassId);

            var result = new ClassWithStudentsDto
            {
                Id = @class.Id,
                Name = @class.Name,
                CourseId = @class.Course.Id,
                CourseName = @class.Course.Name,
                Students = @class.Students
            };

            return result.ToResultDto();
        }
    }
}