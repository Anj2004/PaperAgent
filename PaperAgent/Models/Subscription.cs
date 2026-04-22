using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PaperAgent.Models
{
    [Table("Subscription")]
    public class Subscription
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int HouseholdId { get; set; }

        [Indexed]
        public int PublicationId { get; set; }

        [NotNull]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [NotNull]
        public int Quantity { get; set; } = 1;

        [NotNull]
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
