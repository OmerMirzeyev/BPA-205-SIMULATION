using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sim7.Areas.Admin.ViewModels.Member;
using Sim7.DAL;
using Sim7.Models;
using Sim7.Utilities;
using System.Threading.Tasks;

namespace Sim7.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MemberController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public MemberController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Member> members = await _db.Members
                .Include(m => m.Position)
                .ToListAsync();
            return View(members);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberVM memberVM)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            if (memberVM.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "ImageFile is required");
                return View();
            }
            else
            {
                if (!memberVM.ImageFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be an image");
                    return View();
                }
                if (memberVM.ImageFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile", "ImageFile size must be 2 mb");
                    return View();
                }
                if (!ModelState.IsValid) return View(memberVM);

                Member member = new Member()
                {
                    Name = memberVM.Name,
                    Surname = memberVM.Surname,
                    Description = memberVM.Description,
                    PositionId = memberVM.PositionId,
                    ImageUrl = memberVM.ImageFile.SaveImage(_env, "uploads/member")
                };

                await _db.Members.AddAsync(member);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Member member = await _db.Members.FindAsync(id);
            member.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            Member member = await _db.Members.FindAsync(id);
            member.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            Member member = await _db.Members.FindAsync(id);

            UpdateMemberVM memberVM = new UpdateMemberVM()
            {
                Name = member.Name,
                Surname = member.Surname,
                Description = member.Description,
                PositionId = member.PositionId,
                ImageUrl = member.ImageUrl,
            };
            return View(memberVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateMemberVM memberVM)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            if (!ModelState.IsValid) return View(memberVM);

            Member member = await _db.Members.FindAsync(memberVM.Id);

            member.Name = memberVM.Name;
            member.Surname = memberVM.Surname;
            member.Description = memberVM.Description;
            member.PositionId = memberVM.PositionId;
            if (memberVM.ImageFile is not null)
            {
                member.ImageUrl = memberVM.ImageFile.SaveImage(_env, "uploads/product");
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
