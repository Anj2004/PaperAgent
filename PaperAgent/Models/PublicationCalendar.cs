using System;
using System.Collections.Generic;
using System.Text;

namespace PaperAgent.Models
{
    internal class PublicationCalendar
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public DateTime IssueDate { get; set; }
        public bool HasIssue { get; set; } = true;

    }
}
