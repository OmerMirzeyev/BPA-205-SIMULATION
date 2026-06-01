using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sim3.Areas.Admin.ViewModels.Category;
using Sim3.DAL;
using Sim3.Models;
using System.Threading.Tasks;

namespace Sim3.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            Category category = new Category()
            {
                Name = categoryVM.Name,
            };

            await _db.AddAsync(category);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            Category category = await _db.Categories.FindAsync(id);
            category.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            Category category = await _db.Categories.FindAsync(id);
            category.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            Category category = await _db.Categories.FindAsync(id);

            UpdateCategoryVM categoryVM = new UpdateCategoryVM()
            {
                Name = category.Name
            };

            return View(categoryVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryVM categoryVM)
        {
            Category category = await _db.Categories.FindAsync(categoryVM.Id);

            category.Name = categoryVM.Name;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
