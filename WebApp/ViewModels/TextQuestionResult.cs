namespace WebApp.ViewModels
{
    public class TextQuestionResult
    {
        public int QuestionId { get; set; }
        public int Count { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
    }
}