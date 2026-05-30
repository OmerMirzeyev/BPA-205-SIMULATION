using Microsoft.AspNetCore.Identity;

namespace HomeSim3.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
