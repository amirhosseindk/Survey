using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Answer
{
    public class DeleteAnswersOfStudentCommand : BaseRequest<ResultDto<bool>>
    {
        public int QuestionnaireId { get; set; }
        public string StudentId { get; set; }
    }
}