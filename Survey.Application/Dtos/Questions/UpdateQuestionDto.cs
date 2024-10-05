namespace Survey.Application.Dtos.Questions
{
    public class UpdateQuestionDto
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public int Rank { get; set; }
        public List<UpdateMultipleChoiseOptionDto>? Options { get; set; }
    }
}