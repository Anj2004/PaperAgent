using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PaperAgent.Models
{
    [Table("BillLineItem")]
    public class BillLineItem
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int BillId { get; set; }

        [Indexed]
        public int SubscriptionId { get; set; }

        [NotNull]
        public string PublicationName { get; set; }

        [NotNull]
        public decimal PricePerIssue { get; set; }

        [NotNull]
        public int IssuesDelivered { get; set; }

        [NotNull]
        public int IssuesPaused { get; set; }

        [NotNull]
        public decimal LineTotal { get; set; }

    }
}
