using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Questionnaires.GetQuestionnaireByProfessorId
{
    public class GetQuestionnaireByProfessorIdQuery : BaseRequest<ResultDto<IEnumerable<QuestionnaireDto>>>
    {
        public string Id { get; set; }
    }
}