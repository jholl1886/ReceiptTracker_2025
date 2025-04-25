using ReceiptTracker.Client.Services;
using ReceiptTracker.Shared.DTO;
using System.Collections.ObjectModel;

namespace ReceiptTracker.Client.ViewModels
{
    public class ReceiptListViewModel
    {
        private readonly IReceiptService _receiptService; //receipt service dependency

        public ObservableCollection<ReceiptDTO> Receipts { get; } = new(); //holds receipt data, automatically notifies UI when items are added,removed ,changed or collection is refreshed
        public bool IsLoading { get; private set; } //tracks loading state
        public string? ErrorMessage { get; private set; }

        public ReceiptListViewModel(IReceiptService receiptService) //dependency injection, basically on create the framework will pass in the correct implementation of the interface (done in program.cs)
        {
            _receiptService = receiptService; //stores that service in the private field from earlier
        }

        public async Task LoadReceiptsAsync()
        {
            IsLoading = true; //operation is now in progress
            ErrorMessage = null;

            try
            {
                var receipts = await _receiptService.GetAllReceiptsAsync(); //gotta wait for call to get all receipts from the service
                Receipts.Clear(); //just incase clear collection for new data to come in
                foreach (var receipt in receipts) //goes through all of them pulled from the web
                {
                    Receipts.Add(receipt); //adding receipt to collection
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading receipts: {ex.Message}";
            }
            finally
            {
                IsLoading = false; //when operation is done need to reset this to false no matter outcome
            }
        }

        public async Task DeleteReceiptAsync(string id) //will delete a receipt by giving Id
        {
            try
            {
                bool success = await _receiptService.DeleteReceiptAsync(id); //wait for service to see if receipt is deleted
                if (success)
                {
                    var receiptToRemove = Receipts.FirstOrDefault(r => r.Id == id); //find that receipt in the obv collection and store it, FoD and lamda expression in place of for loop.
                    if (receiptToRemove != null)
                    {
                        Receipts.Remove(receiptToRemove); //if the receipt that was deleted from the api is in the obv collection delete it from thereZX
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