using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;
using SmartPortalApp.Services;

namespace SmartPortalApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseService _courseService;
        private readonly ApplicationDbContext _context;

        public CoursesController(CourseService courseService, ApplicationDbContext context)
        {
            _courseService = courseService;
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return View(courses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Course course)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);
            //    return View(course);
            //}
            course.CreatedById = "Admin";
            course.CreatedOn = DateTime.Now;

            if (!await _courseService.AddCourseAsync(course))
            {
                ModelState.AddModelError("", "Course Code or Course Name must be unique.");
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);
                return View(course);
            }

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Create", "TeachingSubjects");
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.CourseId) return BadRequest();

            //if (!ModelState.IsValid)
            //{
            //    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);
            //    return View(course);
            //}

            course.ModifiedById = "Admin";
            course.ModifiedOn = DateTime.Now;
            if (!await _courseService.UpdateCourseAsync(course))
            {
                ModelState.AddModelError("", "Course Code or Course Name must be unique.");
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", course.DepartmentId);
                return View(course);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await _courseService.DeleteCourseAsync(id)) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
