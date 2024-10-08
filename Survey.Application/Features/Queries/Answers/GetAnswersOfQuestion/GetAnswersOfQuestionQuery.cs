using Survey.Application.Dtos.Answer;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Answers.GetAnswersOfQuestion
{
    public class GetAnswersOfQuestionQuery : BaseRequest<ResultDto<IEnumerable<AnswerDto>>>
    {
        public int QuestionnaireId { get; set; }
        public int QuestionId { get; set; }
    }
}