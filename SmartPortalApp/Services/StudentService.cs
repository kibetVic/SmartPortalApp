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

        public async Task<bool> AddStudentAsync(Student newStudent)
        {
            // Check if a student with the same Identity or RegistrationNo already exists
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.Identity == newStudent.Identity || s.RegistrationNo == newStudent.RegistrationNo);

            if (existingStudent != null)
            {
                return false; // Identity or RegistrationNo already exists
            }

            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
