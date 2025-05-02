using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class TransferService
    {
        private readonly ApplicationDbContext _context;

        public TransferService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all transfers (optionally include related student and course data)
        public async Task<List<Transfer>> GetAllAsync()
        {
            return await _context.Transfers
                .Include(t => t.Student)
                    .ThenInclude(s => s.Course)
                .ToListAsync();
        }

        // Get a single transfer by ID
        public async Task<Transfer?> GetByIdAsync(int id)
        {
            return await _context.Transfers
                .Include(t => t.Student)
                .FirstOrDefaultAsync(t => t.TransferId == id);
        }

        // Get transfers for a specific student
        public async Task<List<Transfer>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Transfers
                .Where(t => t.StudentId == studentId)
                .Include(t => t.Student)
                .ToListAsync();
        }

        // Add new transfer
        public async Task<bool> AddAsync(Transfer transfer)
        {
            _context.Transfers.Add(transfer);
            await _context.SaveChangesAsync();
            return true;
        }

        // Update existing transfer
        public async Task<bool> UpdateAsync(Transfer transfer)
        {
            var existing = await _context.Transfers.FindAsync(transfer.TransferId);
            if (existing == null) return false;

            existing.FromCourseId = transfer.FromCourseId;
            existing.ToCourseId = transfer.ToCourseId;
            existing.UploadKCSEResult = transfer.UploadKCSEResult;
            existing.UploadKCPEResult = transfer.UploadKCPEResult;
            existing.Reason = transfer.Reason;
            existing.TransferStatus = transfer.TransferStatus;
            existing.DeanApproval = transfer.DeanApproval;
            existing.ChairamnApproval = transfer.ChairamnApproval;

            await _context.SaveChangesAsync();
            return true;
        }

        // Delete transfer
        public async Task<bool> DeleteAsync(int id)
        {
            var transfer = await _context.Transfers.FindAsync(id);
            if (transfer == null) return false;

            _context.Transfers.Remove(transfer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
