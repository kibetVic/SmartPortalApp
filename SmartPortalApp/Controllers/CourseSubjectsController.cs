using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Controllers
{
    public class CourseSubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseSubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseSubjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseSubjects.ToListAsync());
        }

        // GET: CourseSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSubject = await _context.CourseSubjects
                .FirstOrDefaultAsync(m => m.CourseSubjectsId == id);
            if (courseSubject == null)
            {
                return NotFound();
            }

            return View(courseSubject);
        }

        // GET: CourseSubjects/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name");
            ViewData["GradeId"] = new SelectList(_context.Grades, "GradeId", "Name");
            return View();
        }

        // POST: CourseSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CourseSubject courseSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseSubject);
        }

        // GET: CourseSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSubject = await _context.CourseSubjects.FindAsync(id);
            if (courseSubject == null)
            {
                return NotFound();
            }
            return View(courseSubject);
        }

        // POST: CourseSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseSubject courseSubject)
        {
            if (id != courseSubject.CourseSubjectsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseSubjectExists(courseSubject.CourseSubjectsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courseSubject);
        }

        // GET: CourseSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSubject = await _context.CourseSubjects
                .FirstOrDefaultAsync(m => m.CourseSubjectsId == id);
            if (courseSubject == null)
            {
                return NotFound();
            }

            return View(courseSubject);
        }

        // POST: CourseSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseSubject = await _context.CourseSubjects.FindAsync(id);
            if (courseSubject != null)
            {
                _context.CourseSubjects.Remove(courseSubject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseSubjectExists(int id)
        {
            return _context.CourseSubjects.Any(e => e.CourseSubjectsId == id);
        }
    }
}
