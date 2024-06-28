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
        public List<int> SelectedClassIds { get; set; } = new List<int>();

        public List<Class> Classes { get; set; }

        public bool IsProfessor { get; set; }
    }
}