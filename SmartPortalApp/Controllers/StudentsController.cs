using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SmartPortalApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        [Authorize]
        public class StudentController : Controller
        {
            private readonly ApplicationDbContext _context;

            public StudentController(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IActionResult> MyProfile()
            {
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userIdStr == null)
                    return Unauthorized();

                int userId = int.Parse(userIdStr);

                var student = await _context.Students
                    .Include(s => s.User)
                    .Include(s => s.Course)
                    .FirstOrDefaultAsync(s => s.UserId == userId);

                if (student == null)
                    return NotFound("Student record not found.");

                return View(student);
            }
        }

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Students.Include(s => s.Course).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }
        /// <summary>
        /// fetch student information
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Information()
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
            var genders = new List<Gender>
    {
        new Gender { GenderId = 1, Name = "MALE" },
        new Gender { GenderId = 2, Name = "FEMALE" }
    };

           
            var applicationDbContext = _context.Students.Include(s => s.Course).Where(s=>s.UserId==id).ToListAsync();
            ViewBag.Genders = genders;
            return View(await applicationDbContext);
        }
        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Course)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", student.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", student.UserId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", student.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", student.UserId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", student.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", student.UserId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Course)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
        // GET: Students/Edit/5
        public async Task<IActionResult> EditInformation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            int idd = 0;
            var userName = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;

            if (!string.IsNullOrEmpty(userName))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
                if (user != null)
                {
                    idd = user.UserId; // Assuming Id is the int field you want
                }
            }
            var randomGender = new List<Gender>
{
    new Gender { GenderId = 1, Name = "MALE" },
    new Gender { GenderId = 2, Name = "FEMALE" },
   
};
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", student.CourseId);
            ViewData["GenderId"] = new SelectList(randomGender, "GenderId", "Name", student.GenderId);
            ViewData["UserId"] = new SelectList(_context.Users.Where(u=>u.UserId==idd), "UserId", "Username", student.UserId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInformation(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Upload KCSE
                    if (student.UploadKCSEFile != null && student.UploadKCSEFile.Length > 0)
                    {
                        var kcseFileName = Guid.NewGuid() + "_" + Path.GetFileName(student.UploadKCSEFile.FileName);
                        var kcseFilePath = Path.Combine(uploadsFolder, kcseFileName);
                        using (var stream = new FileStream(kcseFilePath, FileMode.Create))
                        {
                            await student.UploadKCSEFile.CopyToAsync(stream);
                        }
                        student.UploadKCSE = "/uploads/" + kcseFileName;
                    }

                    // Upload KCPE
                    if (student.UploadKCPEFile != null && student.UploadKCPEFile.Length > 0)
                    {
                        var kcpeFileName = Guid.NewGuid() + "_" + Path.GetFileName(student.UploadKCPEFile.FileName);
                        var kcpeFilePath = Path.Combine(uploadsFolder, kcpeFileName);
                        using (var stream = new FileStream(kcpeFilePath, FileMode.Create))
                        {
                            await student.UploadKCPEFile.CopyToAsync(stream);
                        }
                        student.UploadKCPE = "/uploads/" + kcpeFileName;
                    }


                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Name", student.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", student.UserId);
            return View(student);
        }
    }
}
