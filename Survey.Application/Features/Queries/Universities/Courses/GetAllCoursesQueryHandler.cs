using MediatR;
using Survey.Application.Dtos.Courses;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.Courses
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, ResultDto<IEnumerable<CourseDto>>>
    {
        private readonly IUniversityService _universityService;

        public GetAllCoursesQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<IEnumerable<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _universityService.GetAllCoursesAsync();

            var result = new List<CourseDto>();

            foreach (var course in courses)
            {
                result.Add(new CourseDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    ProfessorId = course.ProfessorId
                });
            }

            return result.AsEnumerable().ToResultDto();
        }
    }
}