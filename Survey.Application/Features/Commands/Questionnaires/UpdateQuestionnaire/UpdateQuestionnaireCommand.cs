using Survey.Application.Dtos.Questions;
using Survey.Application.Dtos.Result;

namespace Survey.Application.Features.Commands.Questionnaires.UpdateQuestionnaire
{
    public class UpdateQuestionnaireCommand : BaseRequest<ResultDto<bool>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<UpdateQuestionDto> Questions { get; set; }
        public int ClassId { get; set; }
        public string ProffesorId { get; set; }
    }
}