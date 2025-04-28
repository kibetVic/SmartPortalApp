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
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Applications


        public async Task<IActionResult> Index()
        {
            var role = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
            if (role == "STUDENT")
            {
                int userIden=0;
                var user = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;
                User? details = new User();
                if (user != null)
                {
                    details = await _context.Users.FirstOrDefaultAsync(i => i.Username == user);
                    if (details != null)
                    {
                        userIden = details.UserId;
                    }

                }
                var applicationDbContext = _context.Applications.Include(a => a.Course).Include(a => a.Department).Include(a => a.School).Include(a => a.Subject).Include(a => a.User)
               .Where(a => a.User.UserId == userIden);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Applications.Include(a => a.Course).Include(a => a.Department).Include(a => a.School).Include(a => a.Subject).Include(a => a.User)
                ;
                return View(await applicationDbContext.ToListAsync());
            }
            
            
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Course)
                .Include(a => a.Department)
                .Include(a => a.School)
                .Include(a => a.Subject)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create

        public async Task<IActionResult> Create()
        {
            try
            {
                int userIden;
                var user = User.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.Name)?.Value;
                User? details = new User();
                if (user != null)
                {
                    details = await _context.Users.FirstOrDefaultAsync(i => i.Username == user);
                    if (details != null)
                    {
                        userIden = details.UserId;
                    }

                }
                var couseDetails = new Course();
                var studentDetails = await _context.Students.FirstOrDefaultAsync(i => i.UserId == details!.UserId);
                if (studentDetails != null)
                {
                    couseDetails = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == studentDetails!.CourseId);
                }
                else
                {
                    ModelState.AddModelError("Student details Missing", "Add Student details");
                }


                ViewData["CourseId"] = new SelectList(new[] { couseDetails }, "CourseId", "CourseName");
                ViewData["MyCourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
                ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "SchoolName");
                ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
                ViewData["UserId"] = new SelectList(new[] { details }, "UserId", "Username");

                ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ApplicationStatus)));
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }


        }





        //public IActionResult Create()
        //{
        //    ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
        //    ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
        //    ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "SchoolName");
        //    ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
        //    ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");

        //    ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ApplicationStatus)));
        //    return View();
        //}

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Application application)
        {
            application.CreatedById = "Admin";
            application.CreatedOn = DateTime.Now;
            // Upload folder path (make sure "uploads" folder exists under wwwroot)
           //var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Upload KCSE
            if (application.UploadKCSEFile != null && application.UploadKCSEFile.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(application.UploadKCSEFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await application.UploadKCSEFile.CopyToAsync(stream);
                }

                application.UploadKCSE = "/uploads/" + uniqueFileName; // Save relative path
            }
            // Upload KCPE
            if (application.UploadKCPEFile != null && application.UploadKCPEFile.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(application.UploadKCPEFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await application.UploadKCPEFile.CopyToAsync(stream);
                }

                application.UploadKCPE = "/uploads/" + uniqueFileName; // Save relative path
            }

                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", application.CourseId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", application.DepartmentId);
            ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "SchoolName", application.SchoolId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", application.SubjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", application.UserId);
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ApplicationStatus)));
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationId,UserId,SchoolId,DepartmentId,CourseId,SubjectId,UploadKCSE,UploadKCPE,ReasonForTransfer,Status,EligibilityStatus,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] Application application)
        {
            if (id != application.ApplicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    application.ModifiedById = "Admin";
                    application.ModifiedOn = DateTime.Now;
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ApplicationId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", application.CourseId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", application.DepartmentId);
            ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "SchoolName", application.SchoolId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", application.SubjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", application.UserId);
            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ApplicationStatus)), application.Status);
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Course)
                .Include(a => a.Department)
                .Include(a => a.School)
                .Include(a => a.Subject)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.ApplicationId == id);
        }
    }
}
