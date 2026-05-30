using HomeSim3.Areas.Admin.ViewModels.Employee;
using HomeSim3.DAL;
using HomeSim3.Models;
using HomeSim3.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeSim3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _db.Employees
                .Include(e => e.Position)
                .ToListAsync();
            return View(employees);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM employeeVM)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();

            if (employeeVM.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Please correct the errors and try again");
                return View();
            }
            else
            {
                if (!employeeVM.ImageFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be an image");
                    return View();
                }
                if (employeeVM.ImageFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile", "ImageFile size must be 2MB");
                    return View();
                }
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            Employee employee = new Employee()
            {
                Name = employeeVM.Name,
                FullAdress = employeeVM.FullAdress,
                PositionId = employeeVM.PositionId,
                ImageUrl = employeeVM.ImageFile.SaveImage(_env, "uploads/employee"),
            };

            await _db.AddAsync(employee);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Employee employee = await _db.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            employee.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null) return NotFound();
            Employee employee = await _db.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            employee.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();


            if (id == null) return NotFound();

            Employee employee = await _db.Employees.FindAsync(id);

            if (employee == null) return NotFound();

            UpdateEmployeeVM employeeVM = new UpdateEmployeeVM()
            {
                Name = employee.Name,
                FullAdress = employee.FullAdress,
                PositionId = employee.PositionId,
                ImageUrl = employee.ImageUrl,
            };

            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeVM employeeVM)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();


            if (employeeVM.Id == null) return NotFound();

            Employee employee = await _db.Employees.FindAsync(employeeVM.Id);

            if (employee == null) return NotFound();

            employee.Name = employeeVM.Name;
            employee.FullAdress = employeeVM.FullAdress;
            employee.PositionId = employeeVM.PositionId;
            if (employeeVM.ImageFile is not null)
            {
                employee.ImageUrl = employeeVM.ImageFile.SaveImage(_env, "uploads/employee");
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}