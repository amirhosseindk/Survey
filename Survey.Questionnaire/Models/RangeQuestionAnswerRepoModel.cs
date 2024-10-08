namespace Survey.Questionnaires.Models
{
    public class RangeQuestionAnswerRepoModel
    {
        public int QuestionnaireId { get; set; }
        public int QuestionId { get; set; }
        public string StudentId { get; set; }
        public short AnswerValue { get; set; }
        public DateTime FillDateTime { get; set; }
    }
}