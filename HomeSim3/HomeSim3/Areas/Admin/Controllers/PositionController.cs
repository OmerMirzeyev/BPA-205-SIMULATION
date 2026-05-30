using HomeSim3.Areas.Admin.ViewModels.Position;
using HomeSim3.DAL;
using HomeSim3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeSim3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public PositionController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _db.Positions.ToListAsync();
            return View(positions);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionVM positionVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Position position = new Position()
            {
                Name = positionVM.Name,
            };
            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Position position = await _db.Positions.FindAsync(id);
            if (position == null) return NotFound();
            position.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null) return NotFound();
            Position position = await _db.Positions.FindAsync(id);
            if (position == null) return NotFound();
            position.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            Position position = await _db.Positions.FindAsync(id);

            if (position == null) return NotFound();

            UpdatePositionVM positionVM = new UpdatePositionVM()
            {
                Id = position.Id,
                Name = position.Name,
            };

            return View(positionVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePositionVM positionVM)
        {
            if (positionVM.Id == null) return NotFound();

            Position position = await _db.Positions.FindAsync(positionVM.Id);

            if (position == null) return NotFound();

            position.Name = positionVM.Name;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
