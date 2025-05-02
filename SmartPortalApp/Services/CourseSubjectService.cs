using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class CourseSubjectService
    {
        private readonly ApplicationDbContext _context;

        public CourseSubjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all CourseSubject records
        public async Task<List<CourseSubject>> GetAllAsync()
        {
            return await _context.CourseSubjects.ToListAsync();
        }

        // Get a CourseSubject by ID
        public async Task<CourseSubject?> GetByIdAsync(int id)
        {
            return await _context.CourseSubjects.FindAsync(id);
        }

        // Add a new CourseSubject
        public async Task<bool> AddAsync(CourseSubject courseSubject)
        {
            _context.CourseSubjects.Add(courseSubject);
            await _context.SaveChangesAsync();
            return true;
        }

        // Update a CourseSubject
        public async Task<bool> UpdateAsync(CourseSubject courseSubject)
        {
            var existing = await _context.CourseSubjects.FindAsync(courseSubject.CourseSubjectsId);
            if (existing == null) return false;

            existing.CourseId = courseSubject.CourseId;
            existing.SubjectId = courseSubject.SubjectId;
            existing.GradeId = courseSubject.GradeId;

            await _context.SaveChangesAsync();
            return true;
        }

        // Delete a CourseSubject
        public async Task<bool> DeleteAsync(int id)
        {
            var courseSubject = await _context.CourseSubjects.FindAsync(id);
            if (courseSubject == null) return false;

            _context.CourseSubjects.Remove(courseSubject);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

