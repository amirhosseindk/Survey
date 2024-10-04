using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Universities.Classes
{
    public class CreateClassCommand : BaseRequest<ResultDto<int>>
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
    }
}