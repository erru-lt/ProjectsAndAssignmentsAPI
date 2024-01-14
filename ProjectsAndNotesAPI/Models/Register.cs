using System.ComponentModel.DataAnnotations;

namespace ProjectsAndNotesAPI.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email adress")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password do not match")]
        public string PasswordConfirm { get; set; } = string.Empty;
    }
}
