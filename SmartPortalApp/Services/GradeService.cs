using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class GradeService
    {
        private readonly ApplicationDbContext _context;

        public GradeService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all grades
        public async Task<List<Grade>> GetAllAsync()
        {
            return await _context.Grades.ToListAsync();
        }

        // Get grade by ID
        public async Task<Grade?> GetByIdAsync(int id)
        {
            return await _context.Grades.FindAsync(id);
        }

        // Add new grade
        public async Task<bool> AddAsync(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return true;
        }

        // Update existing grade
        public async Task<bool> UpdateAsync(Grade grade)
        {
            var existing = await _context.Grades.FindAsync(grade.GradeId);
            if (existing == null) return false;

            existing.Name = grade.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        // Delete grade
        public async Task<bool> DeleteAsync(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return false;

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
