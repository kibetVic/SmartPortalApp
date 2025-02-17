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

        public async Task<bool> AddGradeAsync(Grade newGrade)
        {
            var existingGrade = await _context.Grades
                .FirstOrDefaultAsync(g => g.GradeCode == newGrade.GradeCode ||
                                         g.GradeName == newGrade.GradeName);

            if (existingGrade != null)
            {
                return false; // Duplicate GradeCode and GradeName found
            }

            _context.Grades.Add(newGrade);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
