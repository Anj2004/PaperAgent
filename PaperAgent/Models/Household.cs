using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PaperAgent.Models
{
    [Table("Household")]
    public class Household
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Address { get; set; }

        [NotNull]
        public string Phone { get; set; }

        [NotNull]
        public bool IsActive { get; set; } = true;

        [NotNull]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
