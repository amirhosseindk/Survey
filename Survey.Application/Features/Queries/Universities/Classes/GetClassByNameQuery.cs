using Survey.Application.Dtos.Classes;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.Classes
{
    public class GetClassByNameQuery : BaseRequest<ResultDto<ClassDto>>
    {
        public string ClassName { get; set; }
    }
}