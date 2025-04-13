using ReceiptTracker.Shared.DTO;

namespace ReceiptTracker.API.Services
{
    public interface IReceiptService
    {
        //wanted to use interfaces this time cause watched a youtube video awhile ago that said " you want to write code that calls the interface not the actual class" loosely coupled
        Task<IEnumerable<ReceiptDTO>> GetAllReceiptsAsync();
        Task<ReceiptDTO?> GetReceiptByIdAsync(string id);
        Task<ReceiptDTO> AddReceiptAsync(ReceiptDTO receipt, byte[]? fileData = null, string? fileContentType = null);
        Task<bool> UpdateReceiptAsync(ReceiptDTO receipt);
        Task<bool> DeleteReceiptAsync(string id);
    }
}