using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class TeachingSubjectService
    {
        private readonly ApplicationDbContext _context;

        public TeachingSubjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeachingSubject>> GetAllTeachingSubjectsAsync()
        {
            return await _context.TeachingSubjects.Include(ts => ts.Course).ToListAsync();
        }

        public async Task<TeachingSubject?> GetTeachingSubjectByIdAsync(int id)
        {
            return await _context.TeachingSubjects.Include(ts => ts.Course)
                .FirstOrDefaultAsync(ts => ts.TeachingSubjectId == id);
        }

        public async Task<bool> AddTeachingSubjectAsync(TeachingSubject teachingSubject)
        {
            try
            {
                _context.TeachingSubjects.Add(teachingSubject);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false; // Handle uniqueness violation or other database errors
            }
        }

        public async Task<bool> UpdateTeachingSubjectAsync(TeachingSubject teachingSubject)
        {
            try
            {
                _context.Entry(teachingSubject).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false; // Handle uniqueness violation or other database errors
            }
        }

        public async Task<bool> DeleteTeachingSubjectAsync(int id)
        {
            var teachingSubject = await GetTeachingSubjectByIdAsync(id);
            if (teachingSubject == null) return false;

            _context.TeachingSubjects.Remove(teachingSubject);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
