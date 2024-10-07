namespace Survey.Application.Dtos.Answer
{
    public class CreateAnswerDto
    {
        public int QuestionId { get; set; }
        public string StudentId { get; set; }
        public DateTime FillDateTime { get; set; }
        public int Type { get; set; }
        public int? AnswerOptionId { get; set; }
        public string? AnswerText { get; set; }
        public short? AnswerValue { get; set; }
    }
}