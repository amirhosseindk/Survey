namespace Survey.Questionnaires.Models
{
    public class TextQuestionAnswer
    {
        public int QuestionnaireId { get; set; }
        public int QuestionId { get; set; }
        public string StudentId { get; set; }
        public string AnswerText { get; set; }
        public DateTime FillDateTime { get; set; }
    }
}