using System.ComponentModel.DataAnnotations;

namespace AuthProject.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Full name")]
        public string FullName { get; set; } = string.Empty;

        public int Age { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public List<string> AllRoles { get; set; } = new();
        public List<string> SelectedRoles { get; set; } = new();

    }
}