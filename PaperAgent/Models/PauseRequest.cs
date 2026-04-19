using System;
using System.Collections.Generic;
using System.Text;

namespace PaperAgent.Models
{
    internal class PauseRequest
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Reason { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
