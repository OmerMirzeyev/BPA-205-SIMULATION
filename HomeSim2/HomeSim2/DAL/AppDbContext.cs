using HomeSim2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeSim2.DAL
{
        public class AppDbContext : IdentityDbContext<AppUser>
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<City> Cities { get; set; }
            public DbSet<Place> Places { get; set; }
        }
}
