using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class MinimumRequirementService
    {
        private readonly ApplicationDbContext _context;

        public MinimumRequirementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MinimumRequirement>> GetAllMinimumRequirementsAsync()
        {
            return await _context.MinimumRequirements.Include(m => m.Course).ToListAsync();
        }

        public async Task<MinimumRequirement?> GetMinimumRequirementByIdAsync(int id)
        {
            return await _context.MinimumRequirements.Include(m => m.Course)
                .FirstOrDefaultAsync(m => m.MinimumRequirementId == id);
        }

        public async Task<bool> AddMinimumRequirementAsync(MinimumRequirement minimumRequirement)
        {
            try
            {
                _context.MinimumRequirements.Add(minimumRequirement);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false; // Handle uniqueness violation or other database errors
            }
        }

        public async Task<bool> UpdateMinimumRequirementAsync(MinimumRequirement minimumRequirement)
        {
            try
            {
                _context.Entry(minimumRequirement).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false; // Handle uniqueness violation or other database errors
            }
        }

        public async Task<bool> DeleteMinimumRequirementAsync(int id)
        {
            var minimumRequirement = await GetMinimumRequirementByIdAsync(id);
            if (minimumRequirement == null) return false;

            _context.MinimumRequirements.Remove(minimumRequirement);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
