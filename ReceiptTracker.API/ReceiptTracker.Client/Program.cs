using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReceiptTracker.Client;
using ReceiptTracker.Client.Services;
using ReceiptTracker.Client.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7147") });

// Register Services
//builder.Services.AddScoped<IReceiptService, ReceiptService>();

// Register ViewModels
builder.Services.AddScoped<ReceiptListViewModel>();
builder.Services.AddScoped<ReceiptFormViewModel>();

await builder.Build().RunAsync();
