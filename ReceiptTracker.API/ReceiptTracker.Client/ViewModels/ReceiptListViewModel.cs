using ReceiptTracker.Client.Services;
using ReceiptTracker.Shared.DTO;
using System.Collections.ObjectModel;

namespace ReceiptTracker.Client.ViewModels
{
    public class ReceiptListViewModel
    {
        private readonly IReceiptService _receiptService;

        public ObservableCollection<ReceiptDTO> Receipts { get; } = new();
        public bool IsLoading { get; private set; }
        public string? ErrorMessage { get; private set; }

        public ReceiptListViewModel(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        public async Task LoadReceiptsAsync()
        {
            IsLoading = true;
            ErrorMessage = null;

            try
            {
                var receipts = await _receiptService.GetAllReceiptsAsync();
                Receipts.Clear();
                foreach (var receipt in receipts)
                {
                    Receipts.Add(receipt);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading receipts: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task DeleteReceiptAsync(string id)
        {
            try
            {
                bool success = await _receiptService.DeleteReceiptAsync(id);
                if (success)
                {
                    var receiptToRemove = Receipts.FirstOrDefault(r => r.Id == id);
                    if (receiptToRemove != null)
                    {
                        Receipts.Remove(receiptToRemove);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error deleting receipt: {ex.Message}";
            }
        }
    }
}