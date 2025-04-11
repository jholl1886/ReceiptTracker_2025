using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReceiptTracker.ConsoleApp.Models;
using ReceiptTracker.ConsoleApp.Services;

ReceiptServiceProxy receiptService = new ReceiptServiceProxy(); //remember always create isntance of service

int check = 0;
while (check == 0)
{
    Console.WriteLine("Select you option");
    Console.WriteLine("1: Add a recipe");
    Console.WriteLine("2: See all recipes");
    int choice = Convert.ToInt32(Console.ReadLine());
    if (choice == 1)
    {
        Console.WriteLine("Enter Receipt date (MM/DD/YYYY):");
        DateTime date = Convert.ToDateTime(Console.ReadLine());

        Console.WriteLine("Enter Receipt amount");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine("Enter Receipt Description");
        string description = Console.ReadLine();

        Receipt newReceipt = new Receipt //creation of new receipt object 
        {
            Date = date,
            Amount = amount,
            Description = description
        };

        receiptService.AddReceipt(newReceipt); //use service object to call add function
    }
    else if (choice == 2)
    {
        var allReceipts = receiptService.GetAllReceipts();
        foreach (var receipt in allReceipts)
        {
            Console.WriteLine($"Date: {receipt.Date.ToShortDateString()}");
            Console.WriteLine($"Amount: ${receipt.Amount}");
            Console.WriteLine($"Description: {receipt.Description}");
            Console.WriteLine("------------------------");
        }
    }
    else
    {
        Console.WriteLine("See you later");
        check = 1;
    }

}
