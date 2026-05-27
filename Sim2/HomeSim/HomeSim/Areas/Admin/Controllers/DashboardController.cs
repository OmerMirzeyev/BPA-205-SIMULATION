using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeSim.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Superadmin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
