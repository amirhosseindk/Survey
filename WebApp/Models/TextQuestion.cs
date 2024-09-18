namespace WebApp.Models
{
    public class TextQuestion : Question
    {
        public TextQuestion() { }

        public TextQuestion(short rank, string title)
        {
            Type = QuestionType.Text;
            Title = title;
            Rank = rank;
        }
    }
}