using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Address
    {
        public Address()
        {
            Agenda = new HashSet<Agendum>();
            LeaveRequests = new HashSet<LeaveRequest>();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string ZipCode { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string House { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Agendum> Agenda { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
