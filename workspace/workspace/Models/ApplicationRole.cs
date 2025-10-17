using Microsoft.AspNetCore.Identity;

namespace workspace.Models
{
    // Custom Role class inheriting from IdentityRole
    public class ApplicationRole : IdentityRole
    {
        // You can add additional properties to the ApplicationRole if needed
        public string Description { get; set; }
    }
}
