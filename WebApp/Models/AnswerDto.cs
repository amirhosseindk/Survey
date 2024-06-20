namespace WebApp.Models
{
    public class AnswerDto
    {
        public int QuestionnaireId { get; set; }
        public int QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public int? AnswerOptionId { get; set; }
        public int StudentId { get; set; }
    }
}