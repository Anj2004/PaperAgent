using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PaperAgent.Models
{
    [Table("PauseRequest")]
    public class PauseRequest
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int SubscriptionId { get; set; }

        [NotNull]
        public DateTime FromDate { get; set; }

        [NotNull]
        public DateTime ToDate { get; set; }
        public string Reason { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
