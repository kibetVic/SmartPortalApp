using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using SmartPortalApp.Data;
using SmartPortalApp.Models;
using System.Threading.Tasks;

namespace SmartPortalApp.Controllers
{
    public class CourseTransferController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseTransferController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CourseTransfers;
            return View(await applicationDbContext.ToListAsync());
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
            var studentDetails = await _context.Students.FirstOrDefaultAsync(i => i.UserId == details!.UserId);
            var couseDetails = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == studentDetails!.CourseId); 

            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewData["MyCourseId"] = new SelectList(new[] { couseDetails }, "CourseId", "CourseName");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "SchoolName");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            ViewData["UserId"] = new SelectList(new [] {details}, "UserId", "Username");

            ViewBag.StatusList = new SelectList(Enum.GetValues(typeof(ApplicationStatus)));
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transfer application)
        {
            application.AuditId = "Admin";
            application.DateCreated = DateTime.Now;
        
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

            var studentDetails = await _context.Students.FirstOrDefaultAsync(i => i.UserId == details!.UserId);
            var couseDetails = await _context.Courses.FirstOrDefaultAsync(i => i.CourseId == studentDetails!.CourseId);

            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewData["MyCourseId"] = new SelectList(new[] { couseDetails }, "CourseId", "CourseName");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            ViewData["SchoolId"] = new SelectList(_context.Schools, "SchoolId", "SchoolName");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            ViewData["UserId"] = new SelectList(new[] { details }, "UserId", "Username");






            var existingApplication =await _context.CourseTransfers.FirstOrDefaultAsync(c=>c.UserId==details!.UserId && c.ApplicationStatus=="Pending");
            if (existingApplication != null)
            {
                ModelState.AddModelError("", "You have an existing application pending approval");
                return View(application);
            }
            else if (application!.ToCourse == application.ToCourse)
            {
                ModelState.AddModelError("", "You cannot transfer to the same course");
                return View(application);
            }
            else
            {
                _context.CourseTransfers.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
                      
            

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
