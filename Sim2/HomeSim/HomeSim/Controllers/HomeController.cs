using HomeSim.DAL;
using HomeSim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeSim.Controllers
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
            List<Employee> employees = await _db.Employees
                .Include(e => e.Position)
                .ToListAsync();
            return View(employees);
        }
    }
}
