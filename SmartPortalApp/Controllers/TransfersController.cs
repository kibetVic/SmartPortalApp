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
    public class TransfersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransfersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transfers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transfers.Include(t => t.FromCourse).Include(t => t.Student).Include(t => t.ToCourse);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers
                .Include(t => t.FromCourse)
                .Include(t => t.Student)
                .Include(t => t.ToCourse)
                .FirstOrDefaultAsync(m => m.TransferId == id);
            if (transfer == null)
            {
                return NotFound();
            }

            return View(transfer);
        }

        // GET: Transfers/Create
        public IActionResult Create()
        {
            ViewData["FromCourseId"] = new SelectList(_context.Courses, "CourseId", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "RegNo");
            ViewData["ToCourseId"] = new SelectList(_context.Courses, "CourseId", "Name");
            return View();
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Transfer transfer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transfer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromCourseId"] = new SelectList(_context.Courses, "CourseId", "Name", transfer.FromCourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "RegNo", transfer.StudentId);
            ViewData["ToCourseId"] = new SelectList(_context.Courses, "CourseId", "Name", transfer.ToCourseId);
            return View(transfer);
        }

        // GET: Transfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers.FindAsync(id);
            if (transfer == null)
            {
                return NotFound();
            }
            ViewData["FromCourseId"] = new SelectList(_context.Courses, "CourseId", "Name", transfer.FromCourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "RegNo", transfer.StudentId);
            ViewData["ToCourseId"] = new SelectList(_context.Courses, "CourseId", "Name", transfer.ToCourseId);
            return View(transfer);
        }

        // POST: Transfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transfer transfer)
        {
            if (id != transfer.TransferId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transfer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferExists(transfer.TransferId))
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
            ViewData["FromCourseId"] = new SelectList(_context.Courses, "CourseId", "Name", transfer.FromCourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "RegNo", transfer.StudentId);
            ViewData["ToCourseId"] = new SelectList(_context.Courses, "CourseId", "Name", transfer.ToCourseId);
            return View(transfer);
        }

        // GET: Transfers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers
                .Include(t => t.FromCourse)
                .Include(t => t.Student)
                .Include(t => t.ToCourse)
                .FirstOrDefaultAsync(m => m.TransferId == id);
            if (transfer == null)
            {
                return NotFound();
            }

            return View(transfer);
        }

        // POST: Transfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transfer = await _context.Transfers.FindAsync(id);
            if (transfer != null)
            {
                _context.Transfers.Remove(transfer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransferExists(int id)
        {
            return _context.Transfers.Any(e => e.TransferId == id);
        }
    }
}
