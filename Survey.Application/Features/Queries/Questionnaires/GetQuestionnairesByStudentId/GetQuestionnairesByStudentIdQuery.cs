using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Questionnaires.GetQuestionnairesByStudentId
{
    public class GetQuestionnairesByStudentIdQuery : BaseRequest<ResultDto<IEnumerable<QuestionnaireDto>>>
    {
        public string StudentId { get; set; }
    }
}