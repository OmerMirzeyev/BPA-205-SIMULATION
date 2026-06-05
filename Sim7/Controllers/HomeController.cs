using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sim7.DAL;
using Sim7.Models;
using System.Threading.Tasks;

namespace Sim7.Controllers
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
            List<Member> members = await _db.Members
                .Include(m => m.Position)
                .ToListAsync();
            return View(members);
        }
        public async Task<IActionResult> Details(int id)
        {
            Member member = await _db.Members
                .Include(m => m.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(member);
        }
    }
}
