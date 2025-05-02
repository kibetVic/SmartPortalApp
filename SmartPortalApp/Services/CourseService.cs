using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class CourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all courses
        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses
                .Include(c => c.Department)
                .ToListAsync();
        }

        // Get course by ID
        public async Task<Course?> GetCourseByIdAsync(int courseId)
        {
            return await _context.Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.CourseId == courseId);
        }

        // Add new course
        public async Task<bool> AddCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return true;
        }

        // Update existing course
        public async Task<bool> UpdateCourseAsync(Course course)
        {
            var existing = await _context.Courses.FindAsync(course.CourseId);
            if (existing == null) return false;

            existing.Name = course.Name;
            existing.KsceMeanGrade = course.KsceMeanGrade;
            existing.DepartmentId = course.DepartmentId;

            await _context.SaveChangesAsync();
            return true;
        }

        // Delete a course
        public async Task<bool> DeleteCourseAsync(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

