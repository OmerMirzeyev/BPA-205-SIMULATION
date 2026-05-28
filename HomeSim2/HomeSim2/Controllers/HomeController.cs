using HomeSim2.DAL;
using HomeSim2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeSim2.Controllers
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
            List<Place> places = await _db.Places
                .Where(p => !p.IsDeleted)
                .Include(p => p.City)
                .ToListAsync();
            return View(places);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            Place place = await _db.Places
                .Include(p => p.City)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (place is null) return NotFound();

            return View(place);
        }
    }
}
