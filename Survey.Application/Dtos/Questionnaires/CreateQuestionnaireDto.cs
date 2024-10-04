using Survey.Application.Dtos.Questions;

namespace Survey.Application.Dtos.Questionnaires
{
    public class CreateQuestionnaireDto
    {
        public string Title { get; set; }
        public int ClassId { get; set; }
        public List<CreateQuestionDto> Questions { get; set; }
        public string ProffesorId { get; set; }
    }
}