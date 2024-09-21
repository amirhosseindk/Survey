namespace Survey.Questionnaires.Models
{
    public class MultipleChoiceOption
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        public int MultipleChoiceQuestionId { get; set; }
    }
}