namespace WebApp.Models
{
    public class QuestionnaireDto
    {
        public string Title { get; set; }
        public string Class { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}