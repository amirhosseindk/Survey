using Survey.Application.Dtos.Courses;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.Courses
{
    public class GetAllCoursesByProfessorIdQuery : BaseRequest<ResultDto<IEnumerable<CourseDto>>>
    {
        public string ProfessorId { get; set; }
    }
}