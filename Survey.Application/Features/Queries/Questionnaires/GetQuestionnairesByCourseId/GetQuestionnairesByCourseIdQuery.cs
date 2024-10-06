using Survey.Application.Dtos.Questionnaires;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Queries.Questionnaires.GetQuestionnairesByCourseId
{
    public class GetQuestionnairesByCourseIdQuery : BaseRequest<ResultDto<IEnumerable<QuestionnaireDto>>>
    {
        public int CourseId { get; set; }
    }
}