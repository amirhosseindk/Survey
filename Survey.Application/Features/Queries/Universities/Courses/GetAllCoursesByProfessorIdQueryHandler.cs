using MediatR;
using Survey.Application.Dtos.Courses;
using Survey.Application.Dtos.Result;
using Survey.Application.Extensions;
using Survey.University.Contracts;

namespace Survey.Application.Features.Queries.Universities.Courses
{
    public class GetAllCoursesByProfessorIdQueryHandler : IRequestHandler<GetAllCoursesByProfessorIdQuery, ResultDto<IEnumerable<CourseDto>>>
    {
        private readonly IUniversityService _universityService;

        public GetAllCoursesByProfessorIdQueryHandler(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        public async Task<ResultDto<IEnumerable<CourseDto>>> Handle(GetAllCoursesByProfessorIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await _universityService.GetAllCoursesByProfessorIdAsync(request.ProfessorId);
            return courses.Select(c => new CourseDto { Id = c.Id, Name = c.Name, ProfessorId = c.ProfessorId }).AsEnumerable().ToResultDto();
        }
    }
}