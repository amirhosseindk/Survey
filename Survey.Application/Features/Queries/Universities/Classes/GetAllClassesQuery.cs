using Survey.Application.Dtos.Classes;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Universities.Classes
{
    public class GetAllClassesQuery : BaseRequest<ResultDto<IEnumerable<ClassDto>>>
    {
    }
}