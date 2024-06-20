using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class User : IdentityUser
    {
        public string StudentNumber { get; set; }
        public string Field { get; set; }
        public string Class { get; set; }
        public bool IsProfessor { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Questionnaire> Questionnaires { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}