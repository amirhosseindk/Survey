using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string ProfessorId { get; set; }
        public User Professor { get; set; }
        public ICollection<Questionnaire> Questionnaires { get; set; }
    }
}