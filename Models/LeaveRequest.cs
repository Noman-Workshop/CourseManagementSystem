using System;
using System.Collections.Generic;

namespace Models
{
    public partial class LeaveRequest
    {
        public LeaveRequest()
        {
            LeaveRequestReviews = new HashSet<LeaveRequestReview>();
        }

        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid Type { get; set; }
        public DateTime ProposedStartDate { get; set; }
        public DateTime ProposedEndDate { get; set; }
        public int ProposedDays { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public int ActualDays { get; set; }
        public string Purpose { get; set; } = null!;
        public Guid AddressId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? CancelledDate { get; set; }
        public string? CancelledReason { get; set; }
        public Guid? RelieverId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual User Employee { get; set; } = null!;
        public virtual Employee? Reliever { get; set; }
        public virtual LeaveType TypeNavigation { get; set; } = null!;
        public virtual ICollection<LeaveRequestReview> LeaveRequestReviews { get; set; }
    }
}
