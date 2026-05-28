using HomeSim2.Areas.Admin.ViewModels.City;
using HomeSim2.DAL;
using HomeSim2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeSim2.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class CityController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public CityController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<City> cities = await _db.Cities.ToListAsync();
            return View(cities);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCityVM cityVM)
        {
            if (!ModelState.IsValid)
            {
                return View(cityVM);
            }

            City city = new City
            {
                Name = cityVM.Name,
            };


            await _db.Cities.AddAsync(city);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            City city = await _db.Cities.FindAsync(id);
            if (city == null) return NotFound();
            city.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null) return NotFound();
            City city = await _db.Cities.FindAsync(id);
            if (city == null) return NotFound();
            city.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {

            if (id is null) return NotFound();

            City city = await _db.Cities.FindAsync(id);

            if (city == null) return NotFound();

            UpdateCityVM cityVM = new UpdateCityVM
            {
                Name = city.Name,
            };

            return View(cityVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCityVM cityVM)
        {

            if (!ModelState.IsValid)
            {
                return View(cityVM);
            }

            City city = await _db.Cities.FindAsync(cityVM.Id);

            if (city == null) return NotFound();

            city.Name = cityVM.Name;


            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
