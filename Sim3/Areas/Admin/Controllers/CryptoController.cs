using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sim3.Areas.Admin.ViewModels.Category;
using Sim3.Areas.Admin.ViewModels.Crypto;
using Sim3.DAL;
using Sim3.Models;
using Sim3.Utilities;
using System.Threading.Tasks;

namespace Sim3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CryptoController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public CryptoController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Crypto> cryptos = await _db.Cryptos
                .Include(c => c.Category)
                .ToListAsync();
            return View(cryptos);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCryptoVM cryptoVM)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            if (cryptoVM.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Please correct the error");
                return View();
            }
            else
            {
                if (!cryptoVM.ImageFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("ImageFile", "Please correct the image");
                    return View();
                }
                if (cryptoVM.ImageFile.Length > 2*1024*1024)
                {
                    ModelState.AddModelError("ImageFile", "Please correct the size");
                    return View();
                }
            }

            if (!ModelState.IsValid) return View();
            Crypto crypto = new Crypto()
            {
                Name = cryptoVM.Name,
                Price = cryptoVM.Price,
                Description = cryptoVM.Description,
                CategoryId = cryptoVM.CategoryId,
                ImageUrl = cryptoVM.ImageFile.SaveImage(_env, "uploads/crypto")
            };

            await _db.AddAsync(crypto);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            Crypto crypto = await _db.Cryptos.FindAsync(id);
            crypto.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            Crypto crypto = await _db.Cryptos.FindAsync(id);
            crypto.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();

            Crypto crypto = await _db.Cryptos.FindAsync(id);

            UpdateCryptoVM cryptoVM = new UpdateCryptoVM()
            {
                Name = crypto.Name,
                Description = crypto.Description,
                Price = crypto.Price,
                CategoryId = crypto.CategoryId,
                ImageUrl = crypto.ImageUrl,

            };

            return View(cryptoVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCryptoVM cryptoVM)
        {
            Crypto crypto = await _db.Cryptos.FindAsync(cryptoVM.Id);

            crypto.Name = cryptoVM.Name;
            crypto.Description = cryptoVM.Description;
            crypto.Price = cryptoVM.Price;
            crypto.CategoryId = cryptoVM.CategoryId;
            if (cryptoVM.ImageFile is not null)
            {
                crypto.ImageUrl = cryptoVM.ImageFile.SaveImage(_env, "uploads/crypto");
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
