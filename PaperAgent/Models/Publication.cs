using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PaperAgent.Models
{
    [Table("Publication")]
    public class Publication
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Type { get; set; }

        public string Frequency { get; set; }

        [NotNull]
        public decimal PricePerIssue { get; set; }

        [NotNull]
        public decimal PricePerMonth { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
