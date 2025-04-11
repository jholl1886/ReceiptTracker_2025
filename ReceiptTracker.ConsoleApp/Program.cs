using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReceiptTracker.ConsoleApp.Models;
using ReceiptTracker.ConsoleApp.Services;

ReceiptServiceProxy receiptService = new ReceiptServiceProxy();

int check = 0;
while (check == 0)
{
    Console.WriteLine("Select you option");
    Console.WriteLine("1: Add a recipe");
    Console.WriteLine("2: See all recipes");
    int choice = Convert.ToInt32(Console.ReadLine());
    if (choice == 1)
    {
        Console.WriteLine("Enter Receipt date");

        Console.WriteLine("Enter Receipt amount");

        Console.WriteLine("Enter Receipt Description");
    }
    else if (choice == 2)
    {

    }
    else
    {
        Console.WriteLine("See you later");
        check = 1;
    }

}
