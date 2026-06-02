using Microsoft.AspNetCore.Identity;

namespace Sim4.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
