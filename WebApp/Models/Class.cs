using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<User> Students { get; set; } = new List<User>();
        public ICollection<Questionnaire> Questionnaires { get; set; } = new List<Questionnaire>();
    }
}