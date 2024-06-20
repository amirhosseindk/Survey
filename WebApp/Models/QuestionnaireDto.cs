namespace WebApp.Models
{
    public class QuestionnaireDto
    {
        public string QuestionnaireName { get; set; }
        public int CourseId { get; set; }
        public List<QuestionDto> questionDtos { get; set; }
    }
}