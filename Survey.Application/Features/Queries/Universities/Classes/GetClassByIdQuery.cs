using Survey.Application.Dtos.Classes;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.Classes
{
    public class GetClassByIdQuery : BaseRequest<ResultDto<ClassDto>>
    {
        public int ClassId { get; set; }
    }
}