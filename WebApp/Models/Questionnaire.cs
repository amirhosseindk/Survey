using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Questionnaire
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public string ProfessorId { get; set; }
        public User Professor { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}