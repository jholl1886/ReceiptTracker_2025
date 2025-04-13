using ReceiptTracker.Shared.DTO;

namespace ReceiptTracker.Client.Services
{
    public interface IReceiptService
    {
        Task<IEnumerable<ReceiptDTO>> GetAllReceiptsAsync();
        Task<ReceiptDTO?> GetReceiptByIdAsync(string id);
        Task<ReceiptDTO> AddReceiptAsync(ReceiptDTO receipt, byte[]? fileData = null, string? fileContentType = null);
        Task<bool> UpdateReceiptAsync(ReceiptDTO receipt);
        Task<bool> DeleteReceiptAsync(string id);
    }
}

