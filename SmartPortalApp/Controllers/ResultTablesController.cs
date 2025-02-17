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
    public class ResultTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResultTables
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ResultTable.Include(r => r.Grades).Include(r => r.Points).Include(r => r.Subject);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ResultTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultTable = await _context.ResultTable
                .Include(r => r.Grades)
                .Include(r => r.Points)
                .Include(r => r.Subject)
                .FirstOrDefaultAsync(m => m.ResultTableId == id);
            if (resultTable == null)
            {
                return NotFound();
            }

            return View(resultTable);
        }

        // GET: ResultTables/Create
        public IActionResult Create()
        {
            ViewData["GradeId"] = new SelectList(_context.Grades, "GradeId", "GradeName");
            ViewData["PointId"] = new SelectList(_context.Points, "PointId", "AVGPoint");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            return View();
        }

        // POST: ResultTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResultTable resultTable)
        {

            resultTable.CreatedById = "Adminh";
            resultTable.CreatedOn = DateTime.Now;
            _context.Add(resultTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["GradeId"] = new SelectList(_context.Grades, "GradeId", "GradeName", resultTable.GradeId);
            ViewData["PointId"] = new SelectList(_context.Points, "PointId", "AVGPoint", resultTable.PointId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", resultTable.SubjectId);
            return View(resultTable);
        }

        // GET: ResultTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultTable = await _context.ResultTable.FindAsync(id);
            if (resultTable == null)
            {
                return NotFound();
            }
            ViewData["GradeId"] = new SelectList(_context.Grades, "GradeId", "GradeName", resultTable.GradeId);
            ViewData["PointId"] = new SelectList(_context.Points, "PointId", "AVGPoint", resultTable.PointId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", resultTable.SubjectId);
            return View(resultTable);
        }

        // POST: ResultTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResultTable resultTable)
        {
            if (id != resultTable.ResultTableId)
            {
                return NotFound();
            }
            try
            {
                resultTable.ModifiedById = "Adminh";
                resultTable.ModifiedOn = DateTime.Now;
                _context.Update(resultTable);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultTableExists(resultTable.ResultTableId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["GradeId"] = new SelectList(_context.Grades, "GradeId", "GradeName", resultTable.GradeId);
            ViewData["PointId"] = new SelectList(_context.Points, "PointId", "AVGPoint", resultTable.PointId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", resultTable.SubjectId);
            return View(resultTable);
        }

        // GET: ResultTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultTable = await _context.ResultTable
                .Include(r => r.Grades)
                .Include(r => r.Points)
                .Include(r => r.Subject)
                .FirstOrDefaultAsync(m => m.ResultTableId == id);
            if (resultTable == null)
            {
                return NotFound();
            }

            return View(resultTable);
        }

        // POST: ResultTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultTable = await _context.ResultTable.FindAsync(id);
            if (resultTable != null)
            {
                _context.ResultTable.Remove(resultTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultTableExists(int id)
        {
            return _context.ResultTable.Any(e => e.ResultTableId == id);
        }
    }
}
