using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class User : IdentityUser
    {
        public string StudentNumber { get; set; }
        public string Field { get; set; }
        public bool IsProfessor { get; set; }
        public ICollection<Course> EnrolledCourses { get; set; } = new List<Course>();
        public ICollection<Course> TaughtCourses { get; set; } = new List<Course>();
        public ICollection<Questionnaire> Questionnaires { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}