using Microsoft.AspNetCore.Identity;

namespace AuthProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }

        // Stores the creation time in UTC to ensure a consistent global time standard across all servers and time zones.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}