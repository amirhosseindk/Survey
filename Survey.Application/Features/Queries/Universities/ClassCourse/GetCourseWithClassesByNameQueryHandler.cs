using MediatR;
using Survey.Application.Dtos.Courses;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.ClassCourse
{
    public class GetCourseWithClassesByNameQueryHandler : IRequestHandler<GetCourseWithClassesByNameQuery, ResultDto<CourseWithClassesDto>>
    {
        private readonly IUniversityService _universityService;

        public GetCourseWithClassesByNameQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<CourseWithClassesDto>> Handle(GetCourseWithClassesByNameQuery request, CancellationToken cancellationToken)
        {
            var course = await _universityService.GetCourseWithClassesByNameAsync(request.CourseName);

            var result = new CourseWithClassesDto
            {
                Id = course.Id,
                Name = course.Name,
                ProfessorId = course.ProfessorId,
                Classes = course.Classes
            };

            return result.ToResultDto();
        }
    }
}