using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all students with their related user and course info
        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Course)
                .ToListAsync();
        }

        // Get a student by ID
        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.StudentId == id);
        }

        // Get student by logged-in UserId
        public async Task<Student?> GetByUserIdAsync(int userId)
        {
            return await _context.Students
                .Include(s => s.User)
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        // Add a new student
        public async Task<bool> AddAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return true;
        }

        // Update student information
        public async Task<bool> UpdateAsync(Student student)
        {
            var existing = await _context.Students.FindAsync(student.StudentId);
            if (existing == null) return false;

            existing.Surname = student.Surname;
            existing.OtherNames = student.OtherNames;
            existing.RegNo = student.RegNo;
            existing.CourseId = student.CourseId;
            existing.GenderId = student.GenderId;
            existing.Email = student.Email;
            existing.PhoneNo = student.PhoneNo;

            await _context.SaveChangesAsync();
            return true;
        }

        // Delete a student
        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
