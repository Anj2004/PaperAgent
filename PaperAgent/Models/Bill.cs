using System;
using System.Collections.Generic;
using System.Text;

namespace PaperAgent.Models
{
    internal class Bill
    {
       public int Id { get; set; }

       public int HouseholdId { get; set; }
       
       public int BillingMonth { get; set; }

       public int BillingYear { get; set; }

       public decimal TotalAmount { get; set; }
       
       public DateTime GeneratedAt { get; set; } = DateTime.Now;

       public bool IsPaid { get; set; }

       public DateTime? PaidAt { get; set; } 
        
    }
}
