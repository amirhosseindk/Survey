namespace WebApp.Models
{
    public class Question
    {
        public int Id { get; set; }
        public Int16 Rank { get; set; }
        public QuestionType Type { get; set; }
        public string Title { get; set; }
    }
}