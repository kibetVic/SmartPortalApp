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
            int id = 0;
            var userName = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;

            if (!string.IsNullOrEmpty(userName))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
                if (user != null)
                {
                    id = user.UserId; // Assuming Id is the int field you want
                }
            }
            var studentId = await _context.Students.Where(s => s.UserId == id).Select(s => s.StudentId).FirstOrDefaultAsync();
            var applicationDbContext = _context.Transfers.Include(t => t.FromCourse).Include(t => t.Student).Include(t => t.ToCourse).Where(t=>t.StudentId== studentId);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> StudentTransfers()
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
        public async Task<IActionResult> Create()
        {
            int id = 0;
            var userName = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;

            if (!string.IsNullOrEmpty(userName))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
                if (user != null)
                {
                    id = user.UserId; // Assuming Id is the int field you want
                }
            }
            var student= await _context.Students.FirstOrDefaultAsync(s => s.UserId == id);
            ViewData["FromCourseId"] = new SelectList(_context.Courses.Where(d=>d.CourseId== student!.CourseId), "CourseId", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students.Where(s => s.UserId == id), "StudentId", "RegNo");
            ViewData["ToCourseId"] = new SelectList(_context.Courses, "CourseId", "Name");
            return View();
        }
        public IActionResult StudentTransfer()
        {
            ViewData["FromCourseId"] = new SelectList(_context.Courses, "CourseId", "Name");
            ViewData["ToCourseId"] = new SelectList(_context.Courses, "CourseId", "Name");
            return View();
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transfer transfer)
        {
            try {
                if (ModelState.IsValid)
                {
                    int id = 0;
                    var userName = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;

                    if (!string.IsNullOrEmpty(userName))
                    {
                        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
                        if (user != null)
                        {
                            id = user.UserId; // Assuming Id is the int field you want
                        }
                    }
                    var studentId = await _context.Students.Where(s => s.UserId == id).Select(s => s.StudentId).FirstOrDefaultAsync();
                    transfer.StudentId = studentId;
                    var existingTransfers = await _context.Transfers.Where(us => us.StudentId == transfer.StudentId && us.TransferStatus == "Pending").ToListAsync();
                    if (existingTransfers.Any())
                    {
                        ModelState.AddModelError("", $"There is a pending transfer at the moment");
                    }
                    else
                    {
                    
                    var fromCourseGrade = await _context.Courses.Where(p => p.CourseId == transfer.FromCourseId).Select(p => p.KsceMeanGrade).FirstOrDefaultAsync();
                    var toCourseGrade = await _context.Courses.Where(p => p.CourseId == transfer.ToCourseId).Select(p => p.KsceMeanGrade).FirstOrDefaultAsync();
                    if (transfer.FromCourseId == transfer.ToCourseId)
                    {
                        ModelState.AddModelError("", $"Cannot transfer to the same course.");

                        return View();
                    }
                    else if (fromCourseGrade == toCourseGrade)
                    {
                        var fromSubjects = await _context.CourseSubjects.Include(cs => cs.Subject).Include(cs => cs.Grade).Where(co => co.CourseId == transfer.FromCourseId).ToListAsync();
                        var toSubjects = await _context.CourseSubjects.Include(cs => cs.Subject).Include(cs => cs.Grade).Where(co => co.CourseId == transfer.ToCourseId).ToListAsync();
                        // Compare each subject in the target course with source course
                        foreach (var toSubject in toSubjects)
                        {
                            var match = fromSubjects.FirstOrDefault(fs => fs.SubjectId == toSubject.SubjectId);

                            if (match == null)
                            {
                                //ModelState.AddModelError("", $"Subject with ID {toSubject.SubjectId} is not in the original course.");
                                ModelState.AddModelError("", $"Subject '{toSubject.Subject?.Name}' is missing in the original course.");

                                return View();
                            }

                            if (match.GradeId != toSubject.GradeId)
                            {
                                //ModelState.AddModelError("", $"Grade for subject {toSubject.SubjectId} does not match.");
                                ModelState.AddModelError("", $"Grade for subject '{toSubject.Subject?.Name}' is missing in the original course.");

                                return View();
                            }
                        }
                        _context.Transfers.Add(transfer);
                    }
                    else
                    {
                        ModelState.AddModelError("", $"KSCE Mean grade of {toCourseGrade} is required.");
                        return View();
                    }
                }
                        await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromCourseId"] = new SelectList(_context.Courses, "CourseId", "Name", transfer.FromCourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "RegNo", transfer.StudentId);
            ViewData["ToCourseId"] = new SelectList(_context.Courses, "CourseId", "Name", transfer.ToCourseId);
            return View(transfer);
        }
              catch (Exception ex)
            {
                return View(transfer);
            }
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
            
            ViewData["FromCourseId"] = new SelectList(_context.Courses.Where(g=>g.CourseId==transfer.FromCourseId), "CourseId", "Name", transfer.FromCourseId);
            ViewData["StudentId"] = new SelectList(_context.Students.Where(ss=>ss.StudentId==transfer.StudentId), "StudentId", "RegNo", transfer.StudentId);
            ViewData["ToCourseId"] = new SelectList(_context.Courses.Where(gg => gg.CourseId == transfer.ToCourseId), "CourseId", "Name", transfer.ToCourseId);
            return View(transfer);
        }

        // POST: Transfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transfer transfer)
        {
            try 
            {
                if (id != transfer.TransferId)
                {
                    return NotFound();
                }
                var studentDetails =await _context.Transfers.FirstOrDefaultAsync(v=>v.TransferId==id);
                // Detach the tracked entity to avoid tracking conflict
                _context.Entry(studentDetails!).State = EntityState.Detached;
                transfer.StudentId = studentDetails!.StudentId;
                if (ModelState.IsValid)
                {
                    
                        _context.Update(transfer);
                        await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(StudentTransfers));

                }
                ViewData["FromCourseId"] = new SelectList(_context.Courses, "CourseId", "Name", transfer.FromCourseId);
                ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "RegNo", transfer.StudentId);
                ViewData["ToCourseId"] = new SelectList(_context.Courses, "CourseId", "Name", transfer.ToCourseId);
                return View(transfer);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error occured");
                return View();
            }
            
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
