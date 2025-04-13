using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReceiptTracker.ConsoleApp.Models;

namespace ReceiptTracker.ConsoleApp.Services
{
    public class ReceiptServiceProxy
    {
        private List<Receipt> Receipts;

        //Contructor to initialize the list
        public ReceiptServiceProxy() 
        {
            Receipts = new List<Receipt>();
        }

        public void AddReceipt(Receipt receipt)
        {
            
            Receipts.Add(receipt);
        }

        public List<Receipt> GetAllReceipts()
        {
            return Receipts;
        }
    }
}
