using System;
using System.Collections.Generic;
using System.Text;

namespace PaperAgent.Models
{
    internal class BillLineItem
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public int SubscriptionId { get; set; }
        public string PublicationName { get; set; }
        public decimal PricePerIssue { get; set; }
        public int IssuesDelivered { get; set; }
        public int IssuesPaused { get; set; }
        public decimal LineTotal { get; set; }

    }
}
