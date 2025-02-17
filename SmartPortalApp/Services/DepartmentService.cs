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

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.Include(d => d.School).ToListAsync();
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.Include(d => d.School).FirstOrDefaultAsync(d => d.DepartmentId == id);
        }

        public async Task<bool> AddDepartmentAsync(Department department)
        {
            try
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false; // Handle uniqueness violation or other database errors
            }
        }

        public async Task<bool> UpdateDepartmentAsync(Department department)
        {
            try
            {
                _context.Entry(department).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false; // Handle uniqueness violation or other database errors
            }
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await GetDepartmentByIdAsync(id);
            if (department == null) return false;

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
