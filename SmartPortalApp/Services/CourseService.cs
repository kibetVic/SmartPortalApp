using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;
using System.Collections;

namespace SmartPortalApp.Services
{
    public class CourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.Include(c => c.Department).ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.CourseId == id);
        }

        public async Task<bool> AddCourseAsync(Course course)
        {
            // Check for uniqueness
            if (await _context.Courses.AnyAsync(c => c.CourseCode == course.CourseCode || c.CourseName == course.CourseName))
            {
                return false; // Duplicate entry
            }

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            // Check for uniqueness excluding the current course
            if (await _context.Courses.AnyAsync(c =>
                (c.CourseCode == course.CourseCode || c.CourseName == course.CourseName) &&
                c.CourseId != course.CourseId))
            {
                return false; // Duplicate entry
            }

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await GetCourseByIdAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        //internal IEnumerable GetDepartments()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
