using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Questionnaires.GetQuestionnaireByName
{
    public class GetQuestionnaireByTitleQuery : BaseRequest<ResultDto<QuestionnaireDto>>
    {
        public string Title { get; set; }
    }
}