using System;
using System.Collections.Generic;
using System.Text;

namespace PaperAgent.Models
{
    internal class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
