using MediatR;
using Survey.Application.Dtos.Courses;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.ClassCourse
{
    public class GetCourseWithClassesByIdQueryHandler : IRequestHandler<GetCourseWithClassesByIdQuery, ResultDto<CourseWithClassesDto>>
    {
        private readonly IUniversityService _universityService;

        public GetCourseWithClassesByIdQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<CourseWithClassesDto>> Handle(GetCourseWithClassesByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _universityService.GetCourseWithClassesByIdAsync(request.CourseId);

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