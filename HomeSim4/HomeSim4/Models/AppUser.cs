using Microsoft.AspNetCore.Identity;

namespace HomeSim4.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
