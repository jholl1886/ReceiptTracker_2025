using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReceiptTracker.ConsoleApp.Models;

namespace ReceiptTracker.ConsoleApp.DTO
{
    public class ReceiptDTO
    {
        public string? Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public string? FileName { get; set; }

        public ReceiptDTO(Receipt r) 
        {   
            Id = r.Id;
            Date = r.Date;
            Amount = r.Amount;
            Description = r.Description;
            FileName = r.FileName;
        }

        public ReceiptDTO(ReceiptDTO r)
        {
            Id = r.Id;
            Date = r.Date;
            Amount = r.Amount;
            Description = r.Description;
            FileName = r.FileName;
        }

        public ReceiptDTO() { }
    }
}
