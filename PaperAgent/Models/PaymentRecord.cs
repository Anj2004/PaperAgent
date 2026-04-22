using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PaperAgent.Models
{
    [Table("PaymentRecord")]
    public class PaymentRecord
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int BillId { get; set; }

        [NotNull]
        public decimal AmountPaid { get; set; }

        [NotNull]
        public DateTime PaidAt { get; set; } = DateTime.Now;

        [NotNull]
        public string PaymentMode { get; set; }
        public string Notes { get; set; }
    }
}
