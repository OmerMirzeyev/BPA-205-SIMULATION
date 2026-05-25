using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation_2.DAL;
using Simulation_2.Models;
using System.Threading.Tasks;

namespace Simulation_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> product = await _db.Products
                .Include(p => p.Category)
                .ToListAsync();
            return View(product);
        }
        public async Task<IActionResult> Details(int id)
        {
            Product product = await _db.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
            return View(product);
        }
    }
}
