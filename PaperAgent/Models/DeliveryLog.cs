using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PaperAgent.Models
{
    [Table("DeliveryLog")]
    public class DeliveryLog
    {
        [PrimaryKey, AutoIncrement]
        public int Id {  get; set; }

        [Indexed]
        public int SubscriptionId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool WasDelivered { get; set; } = true;
        public string SkippedReason { get; set; }
    }
}
