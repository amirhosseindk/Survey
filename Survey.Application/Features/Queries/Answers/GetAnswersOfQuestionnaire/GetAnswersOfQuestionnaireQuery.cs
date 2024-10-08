using Survey.Application.Dtos.Answer;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Answers.GetAnswersOfQuestionnaire
{
    public class GetAnswersOfQuestionnaireQuery : BaseRequest<ResultDto<IEnumerable<AnswerDto>>>
    {
        public int QuestionnaireId { get; set; }
    }
}