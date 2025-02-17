using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class SubjectService
    {
        private readonly ApplicationDbContext _context;

        public SubjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSubjectAsync(Subject newSubject)
        {
            var existingSubject = await _context.Subjects
                .FirstOrDefaultAsync(s => s.SubjectCode == newSubject.SubjectCode || s.SubjectName == newSubject.SubjectName);

            if (existingSubject != null)
            {
                return false; // Duplicate SubjectCode and SubjectName found
            }

            _context.Subjects.Add(newSubject);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
