using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartPortalApp.Data;
using SmartPortalApp.Models;
using SmartPortalApp.Services;

namespace SmartPortalApp.Controllers
{
    public class MinimumRequirementsController : Controller
    {
        private readonly MinimumRequirementService _minimumRequirementService;
        private readonly ApplicationDbContext _context;

        public MinimumRequirementsController(MinimumRequirementService minimumRequirementService, ApplicationDbContext context)
        {
            _minimumRequirementService = minimumRequirementService;
            _context = context;
        }

        // GET: MinimumRequirements
        public async Task<IActionResult> Index()
        {
            var minimumRequirements = await _minimumRequirementService.GetAllMinimumRequirementsAsync();
            return View(minimumRequirements);
        }

        // GET: MinimumRequirements/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var minimumRequirement = await _minimumRequirementService.GetMinimumRequirementByIdAsync(id);
            if (minimumRequirement == null) return NotFound();

            return View(minimumRequirement);
        }

        // GET: MinimumRequirements/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: MinimumRequirements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( MinimumRequirement minimumRequirement)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", minimumRequirement.CourseId);
            //    return View(minimumRequirement);
            //}
            minimumRequirement.CreatedById = "Admin";
            minimumRequirement.CreatedOn = DateTime.Now;

            if (!await _minimumRequirementService.AddMinimumRequirementAsync(minimumRequirement))
            {
                ModelState.AddModelError("", "Minimum Requirement Code or Name must be unique.");
                ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", minimumRequirement.CourseId);
                return View(minimumRequirement);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: MinimumRequirements/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var minimumRequirement = await _minimumRequirementService.GetMinimumRequirementByIdAsync(id);
            if (minimumRequirement == null) return NotFound();

            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", minimumRequirement.CourseId);
            return View(minimumRequirement);
        }

        // POST: MinimumRequirements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MinimumRequirement minimumRequirement)
        {
            if (id != minimumRequirement.MinimumRequirementId) return BadRequest();

            //if (!ModelState.IsValid)
            //{
            //    ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", minimumRequirement.CourseId);
            //    return View(minimumRequirement);
            //}
            minimumRequirement.ModifiedById = "Admin";
            minimumRequirement.ModifiedOn = DateTime.Now;

            if (!await _minimumRequirementService.UpdateMinimumRequirementAsync(minimumRequirement))
            {
                ModelState.AddModelError("", "Minimum Requirement Code or Name must be unique.");
                ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", minimumRequirement.CourseId);
                return View(minimumRequirement);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: MinimumRequirements/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var minimumRequirement = await _minimumRequirementService.GetMinimumRequirementByIdAsync(id);
            if (minimumRequirement == null) return NotFound();

            return View(minimumRequirement);
        }

        // POST: MinimumRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await _minimumRequirementService.DeleteMinimumRequirementAsync(id)) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
