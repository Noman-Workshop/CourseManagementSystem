using System;
using System.Collections.Generic;

namespace Models
{
    public partial class EmployeeAttendance
    {
        public DateTime Date { get; set; }
        public Guid EmployeeId { get; set; }
        public string Status { get; set; } = null!;
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        public string? Remarks { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Calender DateNavigation { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
    }
}
