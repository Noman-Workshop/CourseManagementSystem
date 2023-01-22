using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Agendum
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public Guid AddressId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Calender DateNavigation { get; set; } = null!;
    }
}
