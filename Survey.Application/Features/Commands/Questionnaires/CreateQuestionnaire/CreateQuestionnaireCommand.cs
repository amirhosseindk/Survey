using Survey.Application.Dtos.Questions;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Questionnaires.CreateQuestionnaire
{
    public class CreateQuestionnaireCommand : BaseRequest<ResultDto<int>>
    {
        public string Title { get; set; }
        public int ClassId { get; set; }
        public List<CreateQuestionDto> Questions { get; set; }
        public string ProffesorId { get; set; }
    }
}