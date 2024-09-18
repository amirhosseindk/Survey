namespace WebApp.Models
{
    public abstract class Question
    {
        public int Id { get; set; }
        public short Rank { get; set; }
        public QuestionType Type { get; set; }
        public string Title { get; set; }
        public int QuestionnaireId { get; set; }
        public Questionnaire Questionnaire { get; set; }
    }
}