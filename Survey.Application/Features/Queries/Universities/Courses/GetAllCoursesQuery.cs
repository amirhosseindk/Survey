using Survey.Application.Dtos.Courses;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.Courses
{
    public class GetAllCoursesQuery : BaseRequest<ResultDto<IEnumerable<CourseDto>>>
    {
    }
}