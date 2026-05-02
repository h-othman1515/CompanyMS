using System.ComponentModel.DataAnnotations;
namespace AuthProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Full name")]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}