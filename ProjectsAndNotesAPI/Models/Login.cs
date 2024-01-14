using System.ComponentModel.DataAnnotations;

namespace ProjectsAndNotesAPI.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Enter your email adress")]
        [Display(Name = "Email adress")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
