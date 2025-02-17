using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartPortalApp.Data;
using SmartPortalApp.Models;
using SmartPortalApp.Services;

namespace SmartPortalApp.Controllers
{
    public class TeachingSubjectsController : Controller
    {
        private readonly TeachingSubjectService _teachingSubjectService;
        private readonly ApplicationDbContext _context;

        public TeachingSubjectsController(TeachingSubjectService teachingSubjectService, ApplicationDbContext context)
        {
            _teachingSubjectService = teachingSubjectService;
            _context = context;
        }

        // GET: TeachingSubjects
        public async Task<IActionResult> Index()
        {
            var teachingSubjects = await _teachingSubjectService.GetAllTeachingSubjectsAsync();
            return View(teachingSubjects);
        }

        // GET: TeachingSubjects/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var teachingSubject = await _teachingSubjectService.GetTeachingSubjectByIdAsync(id);
            if (teachingSubject == null) return NotFound();

            return View(teachingSubject);
        }

        // GET: TeachingSubjects/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: TeachingSubjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeachingSubject teachingSubject)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", teachingSubject.CourseId);
            //    return View(teachingSubject);
            //}
            teachingSubject.CreatedById = "Admin";
            teachingSubject.CreatedOn = DateTime.Now;

            if (!await _teachingSubjectService.AddTeachingSubjectAsync(teachingSubject))
            {
                ModelState.AddModelError("", "Teaching Subject Code or Name must be unique.");
                ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", teachingSubject.CourseId);
                return View(teachingSubject);
            }

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Create", "MinimumRequirements");
        }

        // GET: TeachingSubjects/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var teachingSubject = await _teachingSubjectService.GetTeachingSubjectByIdAsync(id);
            if (teachingSubject == null) return NotFound();

            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", teachingSubject.CourseId);
            return View(teachingSubject);
        }

        // POST: TeachingSubjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeachingSubject teachingSubject)
        {
            if (id != teachingSubject.TeachingSubjectId) return BadRequest();

            //if (!ModelState.IsValid)
            //{
            //    ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", teachingSubject.CourseId);
            //    return View(teachingSubject);
            //}
            teachingSubject.ModifiedById = "Admin";
            teachingSubject.ModifiedOn = DateTime.Now;

            if (!await _teachingSubjectService.UpdateTeachingSubjectAsync(teachingSubject))
            {
                ModelState.AddModelError("", "Teaching Subject Code or Name must be unique.");
                ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", teachingSubject.CourseId);
                return View(teachingSubject);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: TeachingSubjects/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var teachingSubject = await _teachingSubjectService.GetTeachingSubjectByIdAsync(id);
            if (teachingSubject == null) return NotFound();

            return View(teachingSubject);
        }

        // POST: TeachingSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await _teachingSubjectService.DeleteTeachingSubjectAsync(id)) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
