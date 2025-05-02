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

        // Get all subjects with their related grade
        public async Task<List<Subject>> GetAllAsync()
        {
            return await _context.Subjects
                .Include(s => s.Grade)
                .ToListAsync();
        }

        // Get subject by ID
        public async Task<Subject?> GetByIdAsync(int id)
        {
            return await _context.Subjects
                .Include(s => s.Grade)
                .FirstOrDefaultAsync(s => s.SubjectId == id);
        }

        // Add a new subject
        public async Task<bool> AddAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return true;
        }

        // Update a subject
        public async Task<bool> UpdateAsync(Subject subject)
        {
            var existing = await _context.Subjects.FindAsync(subject.SubjectId);
            if (existing == null) return false;

            existing.Name = subject.Name;
            existing.GradeId = subject.GradeId;

            await _context.SaveChangesAsync();
            return true;
        }

        // Delete a subject
        public async Task<bool> DeleteAsync(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return false;

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
