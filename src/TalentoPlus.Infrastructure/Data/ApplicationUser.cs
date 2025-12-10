using Microsoft.AspNetCore.Identity;

namespace TalentoPlus.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}