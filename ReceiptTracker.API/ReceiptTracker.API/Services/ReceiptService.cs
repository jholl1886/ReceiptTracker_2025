using Microsoft.EntityFrameworkCore;
using ReceiptTracker.API.Data;
using ReceiptTracker.Shared.DTO;
using ReceiptTracker.Shared.Models;

namespace ReceiptTracker.API.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ReceiptService> _logger;

        public ReceiptService(AppDbContext context, ILogger<ReceiptService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<ReceiptDTO>> GetAllReceiptsAsync()
        {
            var receipts = await _context.Receipts.ToListAsync();
            return receipts.Select(r => new ReceiptDTO(r));
        }

        public async Task<ReceiptDTO?> GetReceiptByIdAsync(string id)
        {
            var receipt = await _context.Receipts.FirstOrDefaultAsync(r => r.Id == id);
            return receipt != null ? new ReceiptDTO(receipt) : null;
        }

        public async Task<ReceiptDTO> AddReceiptAsync(ReceiptDTO receiptDto, byte[]? fileData = null, string? fileContentType = null)
        {
            // Convert DTO to domain model
            var receipt = new Receipt
            {
                Id = string.IsNullOrEmpty(receiptDto.Id) ? Guid.NewGuid().ToString() : receiptDto.Id,
                Date = receiptDto.Date,
                Amount = receiptDto.Amount,
                Description = receiptDto.Description,
                FileName = receiptDto.FileName,
                FileData = fileData,
                FileContentType = fileContentType
            };

            await _context.Receipts.AddAsync(receipt);
            await _context.SaveChangesAsync();

            // Return updated DTO with generated ID
            return new ReceiptDTO(receipt);
        }

        public async Task<bool> UpdateReceiptAsync(ReceiptDTO receiptDto)
        {
            // Find existing receipt to update
            var existingReceipt = await _context.Receipts.FindAsync(receiptDto.Id);
            if (existingReceipt == null)
            {
                return false;
            }

            // Update fields from DTO
            existingReceipt.Date = receiptDto.Date;
            existingReceipt.Amount = receiptDto.Amount;
            existingReceipt.Description = receiptDto.Description;

            // Don't update file-related fields here, those are handled separately

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error updating receipt {id}", receiptDto.Id);
                return false;
            }
        }

        public async Task<bool> DeleteReceiptAsync(string id)
        {
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return false;
            }

            _context.Receipts.Remove(receipt);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
