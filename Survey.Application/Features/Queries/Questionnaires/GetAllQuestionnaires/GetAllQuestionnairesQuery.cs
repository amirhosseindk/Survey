using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Questionnaires.GetAllQuestionnaires
{
    public class GetAllQuestionnairesQuery : BaseRequest<ResultDto<IEnumerable<QuestionnaireDto>>>
    {
    }
}