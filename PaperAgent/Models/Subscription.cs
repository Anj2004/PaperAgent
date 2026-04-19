using System;
using System.Collections.Generic;
using System.Text;

namespace PaperAgent.Models
{
    class Subscription
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public int PublicationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int Quantity { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
