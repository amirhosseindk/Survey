namespace Survey.Application.Dtos.Answer
{
    public class UpdateAnswerDto
    {
        public int QuestionId { get; set; }
        public string StudentId { get; set; }
        public DateTime FillDateTime { get; set; }
        public int Type { get; set; }
        public int? AnswerOptionId { get; set; }
        public string? AnswerText { get; set; }
        public int? AnswerValue { get; set; }
        public bool IsDeleted { get; set; }
    }
}