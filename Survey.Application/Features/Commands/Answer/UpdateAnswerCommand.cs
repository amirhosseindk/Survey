using Survey.Application.Dtos.Answer;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Answer
{
    public class UpdateAnswerCommand : BaseRequest<ResultDto<bool>>
    {
        public int QuestionnaireId { get; set; }
        public List<UpdateAnswerDto> Answers { get; set; }
    }
}