using HomeSim4.DAL;
using HomeSim4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeSim4.Controllers
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
                .Where(p => !p.IsDeleted)
                .Include(e => e.Position)
                .ToListAsync();
            return View(employees);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            Employee employee = await _db.Employees
                .Where(p => !p.IsDeleted)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            if (employee is null) return NotFound();
            return View(employee);
        }
    }
}
