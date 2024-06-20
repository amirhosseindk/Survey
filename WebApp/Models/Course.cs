using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfessorId { get; set; }
        public User Professor { get; set; }
        public ICollection<Questionnaire> Questionnaires { get; set; } = new List<Questionnaire>();
        public ICollection<User> Students { get; set; } = new List<User>();
    }
}