using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation_2.Areas.Admin.ViewModels.Product;
using Simulation_2.DAL;
using Simulation_2.Models;
using Simulation_2.Utilities.Extensions;
using System.Threading.Tasks;

namespace Simulation_2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Superadmin")]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _db.Products
                .Include(p => p.Category)
                .ToListAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.ToListAsync();
                return View(productVM);
            }

            Product product = new Product()
            {
                Title = productVM.Title,
                Description = productVM.Description,
                Price = productVM.Price,
                ImageUrl = productVM.ImageFile.SaveImage(_env,"uploads/product"),
                CategoryId = productVM.CategoryId
            };
            await _db.Products.AddAsync(product);
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

            UpdateProductVM productVM = new UpdateProductVM()
            {
                Title = product.Title,
                Description= product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
               
            };
            return View(productVM);
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
