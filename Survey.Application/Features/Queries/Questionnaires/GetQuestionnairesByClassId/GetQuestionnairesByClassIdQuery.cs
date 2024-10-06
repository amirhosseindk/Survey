using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Questionnaires.GetQuestionnairesByClassId
{
    public class GetQuestionnairesByClassIdQuery : BaseRequest<ResultDto<IEnumerable<QuestionnaireDto>>>
    {
        public int ClassId { get; set; }
    }
}