using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PaperAgent.Models
{
    [Table("PublicationCalendar")]
    public class PublicationCalendar
    {
        [PrimaryKey, AutoIncrement]    
        public int Id { get; set; }

        [Indexed]
        public int PublicationId { get; set; }
        public DateTime IssueDate { get; set; }
        public bool HasIssue { get; set; } = true;

    }
}
