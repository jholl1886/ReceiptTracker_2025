using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using ReceiptTracker.Client.Services;
using ReceiptTracker.Shared.DTO;

namespace ReceiptTracker.Client.ViewModels
{
    public class ReceiptFormViewModel
    {
        private readonly IReceiptService _receiptService;
        private readonly NavigationManager _navigationManager;

        public ReceiptDTO Receipt { get; } = new() { Date = DateTime.Today };
        public IBrowserFile? SelectedFile { get; private set; }
        public bool IsSubmitting { get; private set; }
        public string? ErrorMessage { get; private set; }
        public bool IsSuccess { get; private set; }

        public ReceiptFormViewModel(IReceiptService receiptService, NavigationManager navigationManager)
        {
            _receiptService = receiptService;
            _navigationManager = navigationManager;
        }

        public void SetFile(IBrowserFile file)
        {
            SelectedFile = file;
        }

        public async Task SubmitReceiptAsync()
        {
            IsSubmitting = true;
            ErrorMessage = null;
            IsSuccess = false;

            try
            {
                var createdReceipt = await _receiptService.AddReceiptAsync(Receipt, SelectedFile);
                if (createdReceipt != null)
                {
                    IsSuccess = true;
                    // Navigate back to the receipts list

                    resetform(); // just needed to add the reset here 

                    _navigationManager.NavigateTo("/receipts");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error submitting receipt: {ex.Message}";
                IsSuccess = false;
            }
            finally
            {
                IsSubmitting = false;
            }
        }

        public void resetform() // just always add one of these in the future to viewmodels that are forms
        {
            Receipt.Amount = 0;
            Receipt.Description = null;
            Receipt.Date = DateTime.Today;
            SelectedFile = null;
            ErrorMessage = null;
            IsSubmitting = false;
            IsSuccess = false;
        }

    }
}