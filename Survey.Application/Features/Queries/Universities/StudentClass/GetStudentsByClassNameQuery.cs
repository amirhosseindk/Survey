using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.StudentClass
{
    public class GetStudentsByClassNameQuery : BaseRequest<ResultDto<Dictionary<string, string>>>
    {
        public string ClassName { get; set; }
    }
}