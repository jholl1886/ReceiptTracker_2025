using ReceiptTracker.Shared.Models;

namespace ReceiptTracker.API.Services
{
    public interface IReceiptService
    {
        //wanted to use interfaces this time cause watched a youtube video awhile ago that said " you want to write code that calls the interface not the actual class" loosely coupled
        Task<IEnumerable<Receipt>> GetAllReceiptsAsync();
        Task<Receipt?> GetReceiptByIdAsync(string id);
        Task<Receipt> AddReceiptAsync(Receipt receipt);
        Task<bool> UpdateReceiptAsync(Receipt receipt);
        Task<bool> DeleteReceiptAsync(string id);
    }
}