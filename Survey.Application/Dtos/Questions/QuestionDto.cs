namespace Survey.Application.Dtos.Questions
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public short Rank { get; set; }
        public int Type { get; set; }
        public int QuestionnaireId { get; set; }
        public List<MultipleChoiseOptionsDto>? Options { get; set; }
    }
}