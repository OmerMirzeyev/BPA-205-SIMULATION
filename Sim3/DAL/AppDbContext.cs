using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sim3.Models;

namespace Sim3.DAL
{
    public class AppDbContext  : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
