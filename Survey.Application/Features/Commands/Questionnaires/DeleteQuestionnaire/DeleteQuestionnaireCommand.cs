using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Questionnaires.DeleteQuestionnaire
{
    public class DeleteQuestionnaireCommand : BaseRequest<ResultDto<bool>>
    {
        public int Id { get; set; }
    }
}