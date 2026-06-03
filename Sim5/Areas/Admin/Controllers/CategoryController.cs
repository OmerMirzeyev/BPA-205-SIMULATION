using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sim5.Areas.Admin.ViewModels.Category;
using Sim5.DAL;
using Sim5.Models;
using System.Threading.Tasks;

namespace Sim5.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _db.Categories.ToListAsync();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM categoryVM)
        {
            if (!ModelState.IsValid) return View();
            Category category = new Category()
            {
                Name = categoryVM.Name,
            };

            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _db.Categories.FindAsync(id);
            category.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            Category category = await _db.Categories.FindAsync(id);
            category.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (!ModelState.IsValid) return View();
            Category category = await _db.Categories.FindAsync(id);

            UpdateCategoryVM updateVM = new UpdateCategoryVM()
            {
                Name = category.Name
            };

            return View(updateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryVM categoryVM)
        {
            Category category = await _db.Categories.FindAsync(categoryVM.Id);

            category.Name = categoryVM.Name;

            return RedirectToAction(nameof(Index));
        }
    }
}
