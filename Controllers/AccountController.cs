using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simulation_2.DAL;
using Simulation_2.Models;
using Simulation_2.ViewModels.Accounts;

namespace Simulation_2.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDbContext db, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please correct errors and try again");
                return View();
            }

            AppUser user = new AppUser
            {
                UserName = registerVM.UserName,
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    return View(registerVM);
                }
            }


            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login(string? returnURL)
        {
            ViewBag.ReturnUrl = returnURL;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? returnURL, LoginVM loginVM)
        {
            ViewBag.ReturnUrl = returnURL;

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please correct errors and try again");
                return View();
            }

            AppUser user = await _userManager.FindByEmailAsync(loginVM.Email);


            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);



            if (!string.IsNullOrEmpty(returnURL))
            {
                return LocalRedirect(returnURL);
            }


            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
    }
}
