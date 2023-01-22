using System;
using System.Collections.Generic;

namespace Models
{
    public partial class User
    {
        public User()
        {
            LeaveRequests = new HashSet<LeaveRequest>();
            UserRoles = new HashSet<UserRole>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public Guid AddressId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Employee? Employee { get; set; }
        public virtual Student? Student { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
