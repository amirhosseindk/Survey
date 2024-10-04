namespace Survey.Application.Dtos.Questions
{
    public class CreateQuestionDto
    {
        public int Type { get; set; }
        public string Title { get; set; }
        public int Rank { get; set; }
        public List<CreateMultipleChoiseOptionDto>? Options { get; set; }
    }
}