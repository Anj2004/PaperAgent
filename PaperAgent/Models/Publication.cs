using System;
using System.Collections.Generic;
using System.Text;

namespace PaperAgent.Models
{
    class Publication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public string Frequency { get; set; }
        public decimal PricePerIssue { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
