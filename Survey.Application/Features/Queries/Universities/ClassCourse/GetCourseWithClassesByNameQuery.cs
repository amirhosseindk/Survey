using Survey.Application.Dtos.Courses;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.ClassCourse
{
    public class GetCourseWithClassesByNameQuery : BaseRequest<ResultDto<CourseWithClassesDto>>
    {
        public string CourseName { get; set; }
    }
}