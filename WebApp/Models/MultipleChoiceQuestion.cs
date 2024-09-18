namespace WebApp.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion() { }

        public MultipleChoiceQuestion(short rank, string title)
        {
            Rank = rank;
            Title = title;
            Type = QuestionType.MultipleChoice;
        }

        public ICollection<MultipleChoiceOption> Options { get; set; } = new List<MultipleChoiceOption>();
    }
}