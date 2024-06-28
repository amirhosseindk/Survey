using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Student Number")]
        public string StudentNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Class")]
        public string SelectedClassName { get; set; }

        public List<Class>? Classes { get; set; }

        [Display(Name = "Are you a professor?")]
        public bool IsProfessor { get; set; }
    }
}