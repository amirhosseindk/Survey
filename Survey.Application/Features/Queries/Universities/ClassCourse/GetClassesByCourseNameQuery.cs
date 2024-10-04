using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.ClassCourse
{
    public class GetClassesByCourseNameQuery : BaseRequest<ResultDto<Dictionary<int, string>>>
    {
        public string CourseName { get; set; }
    }
}