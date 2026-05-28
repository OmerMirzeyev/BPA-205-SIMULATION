using HomeSim2.Areas.Admin.ViewModels.Place;
using HomeSim2.DAL;
using HomeSim2.Models;
using HomeSim2.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeSim2.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]

    public class PlaceController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public PlaceController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Place> places = await _db.Places
                .Include(p => p.City)
                .ToListAsync();
            return View(places);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.City = await _db.Cities.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePlaceVM placeVM)
        {
            ViewBag.City = await _db.Cities.ToListAsync();

            if (placeVM.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "ImageFile is required");
                return View(placeVM);
            }
            else
            {
                if (!placeVM.ImageFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be an image");
                    return View(placeVM);
                }
                if (placeVM.ImageFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile", "ImageFile size must be 2MB");
                    return View(placeVM);
                }
            }
            if (!ModelState.IsValid)
            {
                return View(placeVM);
            }

            Place place = new Place
            {
                Name = placeVM.Name,
                FullAddress = placeVM.FullAddress,
                CityId = placeVM.CityId,
                ImageURL = placeVM.ImageFile.SaveImage(_env, "uploads/places")
            };

            await _db.Places.AddAsync(place);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Place place = await _db.Places.FindAsync(id);
            if (place == null) return NotFound();
            place.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null) return NotFound();
            Place place = await _db.Places.FindAsync(id);
            if (place == null) return NotFound();
            place.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.City = await _db.Cities.ToListAsync();

            if (id is null) return NotFound();

            Place place = await _db.Places
                .Include(p => p.City)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (place == null) return NotFound();

            UpdatePlaceVM placeVM = new UpdatePlaceVM
            {
                Name = place.Name,
                FullAddress = place.FullAddress,
                CityId = place.CityId,
                ImageURL = place.ImageURL,
            };

            return View(placeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePlaceVM placeVM)
        {
            ViewBag.City = await _db.Cities.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(placeVM);
            }

            Place place = await _db.Places
                .Include(p => p.City)
                .FirstOrDefaultAsync(p => p.Id == placeVM.Id);

            if (place == null) return NotFound();

            place.Name = placeVM.Name;
            place.FullAddress = placeVM.FullAddress;
            place.CityId = placeVM.CityId;


            if (placeVM.ImageFile is not null)
            {
                place.ImageURL = placeVM.ImageFile.SaveImage(_env, "uploads/employee");
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
