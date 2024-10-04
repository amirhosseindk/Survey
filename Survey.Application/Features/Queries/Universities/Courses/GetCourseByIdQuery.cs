using Survey.Application.Dtos.Courses;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.Courses
{
    public class GetCourseByIdQuery : BaseRequest<ResultDto<CourseDto>>
    {
        public int CourseId { get; set; }
    }
}