using Survey.Application.Dtos.Answer;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Answers.GetAnswersOfStudent
{
    public class GetAnswersOfStudentQuery : BaseRequest<ResultDto<IEnumerable<AnswerDto>>>
    {
        public int QuestionnaireId { get; set; }
        public string StudentId { get; set; }
    }
}