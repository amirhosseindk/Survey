using Survey.Questionnaires.Types;

namespace Survey.Questionnaires.Models
{
    public class QuestionRepoModel
    {
        public int Id { get; set; }
        public short Rank { get; set; }
        public QuestionType Type { get; set; }
        public string Title { get; set; }
        public int QuestionnaireId { get; set; }
    }
}