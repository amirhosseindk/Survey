namespace WebApp.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public MultipleChoiceQuestion(Int16 rank, string title)
        {
            Rank = rank;
            Title = title;
            Type = QuestionType.MultipleChoice;
        }
        public List<MultipleChoiceOption> Options { get; set; }
        public Int16 Answer { get; set; }
    }
}