using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation_2.Areas.Admin.ViewModels.Category;
using Simulation_2.Areas.Admin.ViewModels.Product;
using Simulation_2.DAL;
using Simulation_2.Models;
using Simulation_2.Utilities.Extensions;

namespace Simulation_2.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public CategoryController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _db.Products
                .ToListAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM productVM)
        {
           

            Category category = new Category()
            {
                CategoryName = productVM.CategoryName,
            };
            await _db.Products.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _db.Products.FindAsync(id);
            product.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            Product product = await _db.Products.FindAsync(id);
            product.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            Product product = await _db.Products.FindAsync(id);

            ViewBag.Categories = await _db.Categories.ToListAsync();

            UpdateCategoryVM update = new UpdateCategoryVM()
            {
                CategoryName = update.CategoryName
            };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVM productVM)
        {

            Product product = await _db.Products.FindAsync(productVM.Id);

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.ToListAsync();
                return View(productVM);
            }

            product.Title = productVM.Title;
            product.Description = productVM.Description;
            product.Price = productVM.Price;
            product.CategoryId = productVM.CategoryId;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
