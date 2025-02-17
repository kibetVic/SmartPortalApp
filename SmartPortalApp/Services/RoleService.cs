using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class RoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRoleAsync(Role newRole)
        {
            var existingRole = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleCode == newRole.RoleCode || r.RoleName == newRole.RoleName);

            if (existingRole != null)
            {
                return false; // Duplicate RoleCode and RoleName found
            }

            _context.Roles.Add(newRole);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
