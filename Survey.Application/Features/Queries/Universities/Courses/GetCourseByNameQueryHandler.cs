using MediatR;
using Survey.Application.Dtos.Courses;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.Courses
{
    public class GetCourseByNameQueryHandler : IRequestHandler<GetCourseByNameQuery, ResultDto<CourseDto>>
    {
        private readonly IUniversityService _universityService;

        public GetCourseByNameQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<CourseDto>> Handle(GetCourseByNameQuery request, CancellationToken cancellationToken)
        {
            var course = await _universityService.GetCourseByNameAsync(request.CourseName);

            var result = new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                ProfessorId = course.ProfessorId
            };

            return result.ToResultDto();
        }
    }
}