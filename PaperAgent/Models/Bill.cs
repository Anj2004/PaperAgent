using System;
using System.Collections.Generic;
using System.Text;
using SQLite; 

namespace PaperAgent.Models
{
    [Table ("Bill")]
    public  class Bill
    {
       [PrimaryKey, AutoIncrement]
       public int Id { get; set; }

       [Indexed]
       public int HouseholdId { get; set; }
       
       [NotNull] 
       public int BillingMonth { get; set; }

       [NotNull]
       public int BillingYear { get; set; }

       [NotNull]
       public decimal TotalAmount { get; set; }
       
       [NotNull]
       public DateTime GeneratedAt { get; set; } = DateTime.Now;

       [NotNull]
       public bool IsPaid { get; set; }

       public DateTime? PaidAt { get; set; } 
        
    }
}
