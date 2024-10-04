using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.StudentClass
{
    public class GetStudentsByClassIdQuery : BaseRequest<ResultDto<Dictionary<string, string>>>
    {
        public int ClassId { get; set; }
    }
}