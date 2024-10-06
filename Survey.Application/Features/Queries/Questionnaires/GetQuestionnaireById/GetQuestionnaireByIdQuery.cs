using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Questionnaires.GetQuestionnaireById
{
    public class GetQuestionnaireByIdQuery : BaseRequest<ResultDto<QuestionnaireDto>>
    {
        public int Id { get; set; }
    }
}