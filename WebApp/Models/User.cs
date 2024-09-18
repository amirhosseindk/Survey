using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class User : IdentityUser
    {
        public string StudentNumber { get; set; }
        public bool IsProfessor { get; set; }
        public ICollection<Course> TaughtCourses { get; set; } = new List<Course>();
        public ICollection<Class> Classes { get; set; } = new List<Class>();
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}