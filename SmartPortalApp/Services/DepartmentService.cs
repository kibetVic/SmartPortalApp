using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class DepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all departments
        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments
                .Include(d => d.School)
                .ToListAsync();
        }

        // Get department by ID
        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .Include(d => d.School)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);
        }

        // Add a new department
        public async Task<bool> AddAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return true;
        }

        // Update an existing department
        public async Task<bool> UpdateAsync(Department department)
        {
            var existing = await _context.Departments.FindAsync(department.DepartmentId);
            if (existing == null) return false;

            existing.Name = department.Name;
            existing.SchoolId = department.SchoolId;

            await _context.SaveChangesAsync();
            return true;
        }

        // Delete a department
        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

