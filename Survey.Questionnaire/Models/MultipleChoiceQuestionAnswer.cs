namespace Survey.Questionnaires.Models
{
    public class MultipleChoiceQuestionAnswer
    {
        public int QuestionnaireId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerOptionId { get; set; }
        public string StudentId { get; set; }
        public DateTime FillDateTime { get; set; }
    }
}