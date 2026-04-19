using System;
using System.Collections.Generic;
using System.Text;

namespace PaperAgent.Models
{
    internal class DeliveryLog
    {
        public int Id {  get; set; }
        public int SubscriptionId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool WasDelivered { get; set; } = true;
        public string SkippedReason { get; set; }
    }
}
