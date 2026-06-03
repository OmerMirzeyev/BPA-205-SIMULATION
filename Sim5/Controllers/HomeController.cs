using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sim5.DAL;
using Sim5.Models;
using System.Threading.Tasks;

namespace Sim5.Controllers
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
            List<Product> products = await _db.Products
                .Include(p => p.Category)
                .ToListAsync();
            return View(products);
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
