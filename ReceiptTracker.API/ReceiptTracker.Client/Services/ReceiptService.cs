using Microsoft.AspNetCore.Components.Forms;
using ReceiptTracker.Shared.DTO;
using System.Net.Http.Json;


namespace ReceiptTracker.Client.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly HttpClient _httpClient;

        public ReceiptService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ReceiptDTO>> GetAllReceiptsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<ReceiptDTO>>("api/receipts");
            return response ?? Array.Empty<ReceiptDTO>();
        }

        public async Task<ReceiptDTO?> GetReceiptByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<ReceiptDTO>($"api/receipts/{id}");
        }

        public async Task<ReceiptDTO?> AddReceiptAsync(ReceiptDTO receipt, IBrowserFile? file)
        {
            using var content = new MultipartFormDataContent();

            // Add receipt data
            content.Add(new StringContent(receipt.Date.ToString("yyyy-MM-dd")), "Date");
            content.Add(new StringContent(receipt.Amount.ToString()), "Amount");
            content.Add(new StringContent(receipt.Description ?? ""), "Description");

            // Add file if provided
            if (file != null)
            {
                var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 10485760)); // 10MB max
                content.Add(fileContent, "file", file.Name);
            }

            var response = await _httpClient.PostAsync("api/receipts", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReceiptDTO>();
        }

        public async Task<bool> UpdateReceiptAsync(ReceiptDTO receipt)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/receipts/{receipt.Id}", receipt);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteReceiptAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/receipts/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
