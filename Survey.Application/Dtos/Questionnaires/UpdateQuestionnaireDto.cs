using Survey.Application.Dtos.Questions;

namespace Survey.Application.Dtos.Questionnaires
{
    public class UpdateQuestionnaireDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ClassId { get; set; }
        public List<UpdateQuestionDto> Questions { get; set; }
        public string ProffesorId { get; set; }
    }
}