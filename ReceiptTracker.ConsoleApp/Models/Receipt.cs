using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptTracker.ConsoleApp.Models
{
    public  class Receipt
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string? FileName { get; set; }


    }
}
