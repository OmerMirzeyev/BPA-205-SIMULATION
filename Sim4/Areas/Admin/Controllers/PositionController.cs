using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sim4.Areas.Admin.ViewModels.Positon;
using Sim4.DAL;
using Sim4.Models;
using System.Threading.Tasks;

namespace Sim4.Areas.Admin.Controllers
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
            List<Position> positions = await _db.Positions.ToListAsync();
            return View(positions);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionVM positionVM)
        {
            if (!ModelState.IsValid) return View();
            Position position = new Position()
            {
                Name = positionVM.Name,
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
            if (!ModelState.IsValid) return View();
            Position position = await _db.Positions.FindAsync(id);

            UpdatePositionVM updateVM = new UpdatePositionVM()
            {
                Name = position.Name
            };

            return View(updateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePositionVM positionVM)
        {
            Position position = await _db.Positions.FindAsync(positionVM.Id);

            position.Name = positionVM.Name;

            return RedirectToAction(nameof(Index));
        }
    }
}
