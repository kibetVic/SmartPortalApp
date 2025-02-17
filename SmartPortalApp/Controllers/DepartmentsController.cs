using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartPortalApp.Data;
using SmartPortalApp.Models;
using SmartPortalApp.Services;

namespace SmartPortalApp.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly DepartmentService _departmentService;
        private readonly ApplicationDbContext _context;

        public DepartmentsController(DepartmentService departmentService, ApplicationDbContext context)
        {
            _departmentService = departmentService;
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null) return NotFound();

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "SchoolName");
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Department department)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "SchoolName", department.SchoolId);
            //    return View(department);
            //}
            department.CreatedById = "Admin";
            department.CreatedOn = DateTime.Now;

            if (!await _departmentService.AddDepartmentAsync(department))
            {
                ModelState.AddModelError("", "Department Code or Department Name must be unique.");
                ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "SchoolName", department.SchoolId);
                return View(department);
            }

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Create", "Courses");
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null) return NotFound();

            ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "SchoolName", department.SchoolId);
            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.DepartmentId) return BadRequest();

            //if (!ModelState.IsValid)
            //{
            //    ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "SchoolName", department.SchoolId);
            //    return View(department);
            //}
            department.ModifiedById = "Admin";
            department.ModifiedOn = DateTime.Now;

            if (!await _departmentService.UpdateDepartmentAsync(department))
            {
                ModelState.AddModelError("", "Department Code or Department Name must be unique.");
                ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "SchoolName", department.SchoolId);
                return View(department);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null) return NotFound();

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await _departmentService.DeleteDepartmentAsync(id)) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
