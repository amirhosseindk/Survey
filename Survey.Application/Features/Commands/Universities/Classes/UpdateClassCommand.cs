using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Universities.Classes
{
    public class UpdateClassCommand : BaseRequest<ResultDto<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
    }
}
