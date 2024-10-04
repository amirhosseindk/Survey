using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Universities.StudentClass
{
    public class AddStudentToClassCommand : BaseRequest<ResultDto<bool>>
    {
        public string StudentId { get; set; }
        public int ClassId { get; set; }
    }
}