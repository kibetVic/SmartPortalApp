using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class PointService
    {
        private readonly ApplicationDbContext _context;

        public PointService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPointAsync(Point newPoint)
        {
            var existingPoint = await _context.Points
                .FirstOrDefaultAsync(p => p.PointCode == newPoint.PointCode || p.AVGPoint == newPoint.AVGPoint);

            if (existingPoint != null)
            {
                return false; // Duplicate PointCode and AVGPoint found
            }

            _context.Points.Add(newPoint);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
