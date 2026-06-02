using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sim4.Areas.Admin.ViewModels.Member;
using Sim4.DAL;
using Sim4.Models;
using System.Threading.Tasks;

namespace Sim4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MemberController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public MemberController(AppDbContext db,IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
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
            if(memberVM.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "ImageFile is not null");
                return View();
            }
            else
            {
            if (!memberVM.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "ImageFile in valid");
                return View();
            }
            if(memberVM.ImageFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile", "ImageFile size max 2mb");
                    return View();
                }
            }
            if (!ModelState.IsValid) return View();

            Member member = new Member()
            {
                Name = memberVM.Name,
                Surname = memberVM.Surname,
                Description = memberVM.Description,
                PositionId = memberVM.PositionId,
                ImageUrl = memberVM.ImageFile.SaveImage(_environment, "uploads/member")
            };

            await _db.Members.AddAsync(member);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
            Member member = await _db.Members.FindAsync(memberVM.Id);

            member.Name = memberVM.Name;
            member.Surname = memberVM.Surname;
            member.Description = memberVM.Description;
            member.PositionId = memberVM.PositionId;
            if(memberVM.ImageFile is not null)
            {
                member.ImageUrl = memberVM.ImageFile.SaveImage(_environment, "uploads/member");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
