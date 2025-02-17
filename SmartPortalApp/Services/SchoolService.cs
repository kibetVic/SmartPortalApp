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

        public async Task<bool> AddSchoolAsync(School newSchool)
        {
            var existingSchool = await _context.Schools
                .FirstOrDefaultAsync(s => s.SchoolCode == newSchool.SchoolCode || s.SchoolName == newSchool.SchoolName);

            if (existingSchool != null)
            {
                return false; // Duplicate SchoolCode and SchoolName found
            }

            _context.Schools.Add(newSchool);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
