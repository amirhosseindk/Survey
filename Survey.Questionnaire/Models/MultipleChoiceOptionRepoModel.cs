namespace Survey.Questionnaires.Models
{
    public class MultipleChoiceOptionRepoModel
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        public int MultipleChoiceQuestionId { get; set; }
    }
}