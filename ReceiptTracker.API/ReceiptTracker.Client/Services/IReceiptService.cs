using ReceiptTracker.Shared.DTO;
using Microsoft.AspNetCore.Components.Forms;

namespace ReceiptTracker.Client.Services
{
    public interface IReceiptService
    {
        Task<IEnumerable<ReceiptDTO>> GetAllReceiptsAsync();
        Task<ReceiptDTO?> GetReceiptByIdAsync(string id);
        Task<ReceiptDTO?> AddReceiptAsync(ReceiptDTO receipt, IBrowserFile? file);
        Task<bool> UpdateReceiptAsync(ReceiptDTO receipt);
        Task<bool> DeleteReceiptAsync(string id);
    }
}

