using System;
using System.Collections.Generic;
using System.Text;

namespace PaperAgent.Models
{
    internal class PaymentRecord
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaidAt { get; set; } = DateTime.Now;
        public string PaymentMode { get; set; }
        public string Notes { get; set; }
    }
}
