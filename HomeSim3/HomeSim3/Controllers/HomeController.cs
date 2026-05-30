using HomeSim3.DAL;
using HomeSim3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeSim3.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            List<Employee> employees = await _db.Employees
                .Include(e => e.Position)
                .ToListAsync();
            return View(employees);
        }
    }
}
