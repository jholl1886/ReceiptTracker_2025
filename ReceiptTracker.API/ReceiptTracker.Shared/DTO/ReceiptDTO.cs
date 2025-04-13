using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReceiptTracker.Shared.Models;

namespace ReceiptTracker.Shared.DTO
{
    public class ReceiptDTO
    {
        public string? Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public string? FileName { get; set; }

        public byte[]? FileData { get; set; }

        public string? FileContentType { get; set; }

        public ReceiptDTO(Receipt r) 
        {   
            Id = r.Id;
            Date = r.Date;
            Amount = r.Amount;
            Description = r.Description;
            FileName = r.FileName;
            FileContentType = r.FileContentType;
            FileData = r.FileData;
        }

        public ReceiptDTO(ReceiptDTO r)
        {
            Id = r.Id;
            Date = r.Date;
            Amount = r.Amount;
            Description = r.Description;
            FileName = r.FileName;
            FileContentType = r.FileContentType;
            FileData = r.FileData;
        }

        public ReceiptDTO() { }
    }
}
