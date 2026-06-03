
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sim5.Areas.Admin.ViewModels.Product;
using Sim5.DAL;
using Sim5.Models;
using Sim5.Utilities;
using System.Threading.Tasks;

namespace Sim5.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
                .ToListAsync ();
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
            if (productVM.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "ImageFile is required");
                return View();
            }
            else
            {
                if (!productVM.ImageFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be an image");
                    return View();
                }
                if (productVM.ImageFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile", "ImageFile size must be 2 mb");
                    return View();
                }
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            Product product = new Product
            {
                Name = productVM.Name,
                Price = productVM.Price,
                Description = productVM.Description,
                CategoryId = productVM.CategoryId,
                ImageUrl = productVM.ImageFile.SaveImage(_env, "uploads/product")
            };

            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            Product product = await _db.Products.FindAsync(id);
            product.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            Product product = await _db.Products.FindAsync(id);
            product.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            Product product = await _db.Products.FindAsync(id);


            UpdateProductVM productVM = new UpdateProductVM()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,
            };

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVM productVM)
        {
            if (productVM.Id == null) return BadRequest();

            Product product = await _db.Products.FindAsync(productVM.Id);

            if (product == null) return NotFound();

            product.Name = productVM.Name;
            product.Price = productVM.Price;
            product.Description = productVM.Description;
            product.CategoryId = productVM.CategoryId;
            if (productVM.ImageFile is not null)
            {
                product.ImageUrl = productVM.ImageFile.SaveImage(_env, "uploads/product");
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
