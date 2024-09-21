using Microsoft.AspNetCore.Identity;

namespace Survey.Users.Models
{
    public class User : IdentityUser
    {
        public string StudentNumber { get; set; }
        public bool IsProfessor { get; set; }
    }
}