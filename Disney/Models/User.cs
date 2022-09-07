using Microsoft.AspNetCore.Identity;

namespace Disney.Models
{
    public class User : IdentityUser
    {
        public bool IsActive { get; set; }

    }
}
