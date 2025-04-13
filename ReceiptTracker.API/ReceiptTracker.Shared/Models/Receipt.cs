using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReceiptTracker.Shared.DTO;

namespace ReceiptTracker.Shared.Models
{
    public  class Receipt
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string? FileName { get; set; }

        public byte[]? FileData { get; set; }

        public string? FileContentType { get; set; }

        public Receipt (Receipt r)
        {
            Id = r.Id;
            Date = r.Date;
            Amount = r.Amount;
            Description = r.Description;
            FileName = r.FileName;
            FileContentType = r.FileContentType;
            FileData = r.FileData;
            FileContentType= r.FileContentType;
        }

        public Receipt (ReceiptDTO d)
        {
            Id = d.Id;
            Date = d.Date;
            Amount = d.Amount;
            Description = d.Description;
            FileName = d.FileName;
            FileContentType = d.FileContentType;
            FileData = d.FileData;
        }

        public Receipt() { }
    }
}
