using Survey.Application.Dtos.Courses;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.Courses
{
    public class GetCourseByNameQuery : BaseRequest<ResultDto<CourseDto>>
    {
        public string CourseName { get; set; }
    }
}