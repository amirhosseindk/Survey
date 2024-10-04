using Survey.Application.Dtos.Courses;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.ClassCourse
{
    public class GetCourseWithClassesByIdQuery : BaseRequest<ResultDto<CourseWithClassesDto>>
    {
        public int CourseId { get; set; }
    }
}