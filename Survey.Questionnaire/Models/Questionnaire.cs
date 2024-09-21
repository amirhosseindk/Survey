namespace Survey.Questionnaires.Models
{
    public class Questionnaire
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ClassId { get; set; }
        public string ProfessorId { get; set; }
    }
}