using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sim7.Areas.Admin.ViewModels.Member;
using Sim7.Areas.Admin.ViewModels.Position;
using Sim7.DAL;
using Sim7.Models;
using System.Threading.Tasks;

namespace Sim7.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class PositionController : Controller
    {
        private readonly AppDbContext _db;

        public PositionController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Position> positon = await _db.Positions.ToListAsync();
            return View(positon);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionVM positionVM)
        {
            if (!ModelState.IsValid) return View(positionVM);

            Position position = new Position()
            {
                Name = positionVM.Name
            };

            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Position position = await _db.Positions.FindAsync(id);
            position.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            Position position = await _db.Positions.FindAsync(id);
            position.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Position position = await _db.Positions.FindAsync(id);

            UpdatePositionVM positionVM = new UpdatePositionVM()
            {
                Id = position.Id,
                Name = position.Name
            };
            return View(positionVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePositionVM positionVM)
        {
            if (!ModelState.IsValid) return View(positionVM);
            Position position = await _db.Positions.FindAsync(positionVM.Id);

            position.Name = positionVM.Name;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
