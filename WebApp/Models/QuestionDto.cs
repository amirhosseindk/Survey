namespace WebApp.Models
{
    public class QuestionDto
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int? Rank { get; set; }
        public List<string>? Options { get; set; }
    }
}