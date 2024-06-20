namespace WebApp.Models
{
    public class QuestionnaireDto
    {
        public string Title { get; set; }
        public string Course { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}