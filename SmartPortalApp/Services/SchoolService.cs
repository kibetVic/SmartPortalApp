using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class SchoolService
    {
        private readonly ApplicationDbContext _context;

        public SchoolService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all schools
        public async Task<List<School>> GetAllAsync()
        {
            return await _context.Schools.ToListAsync();
        }

        // Get a school by ID
        public async Task<School?> GetByIdAsync(int id)
        {
            return await _context.Schools.FindAsync(id);
        }

        // Add a new school
        public async Task<bool> AddAsync(School school)
        {
            _context.Schools.Add(school);
            await _context.SaveChangesAsync();
            return true;
        }

        // Update an existing school
        public async Task<bool> UpdateAsync(School school)
        {
            var existing = await _context.Schools.FindAsync(school.SchoolId);
            if (existing == null) return false;

            existing.Name = school.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        // Delete a school
        public async Task<bool> DeleteAsync(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null) return false;

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
