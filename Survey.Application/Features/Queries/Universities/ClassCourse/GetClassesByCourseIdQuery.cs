using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.ClassCourse
{
    public class GetClassesByCourseIdQuery : BaseRequest<ResultDto<Dictionary<int, string>>>
    {
        public int CourseId { get; set; }
    }
}