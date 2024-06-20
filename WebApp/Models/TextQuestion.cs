namespace WebApp.Models
{
    public class TextQuestion : Question
    {
        public TextQuestion(Int16 rank, string title) 
        {
            Type = QuestionType.Text;
            Title = title;
            Rank = rank;
        }
        public string Answer { get; set; } = string.Empty;
    }
}