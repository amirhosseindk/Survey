using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Universities.Classes
{
    public class DeleteClassCommand : BaseRequest<ResultDto<bool>>
    {
        public int ClassId { get; set; }
    }
}