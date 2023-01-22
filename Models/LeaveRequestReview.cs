using System;
using System.Collections.Generic;

namespace Models
{
    public partial class LeaveRequestReview
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid ReviewerId { get; set; }
        public int Order { get; set; }
        public string? Comment { get; set; }
        public bool IsApproved { get; set; }
        public DateTime PermittedStartDate { get; set; }
        public DateTime PermittedEndDate { get; set; }
        public int PermittedDays { get; set; }
        public Guid? RelieverId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Employee? Reliever { get; set; }
        public virtual LeaveRequest Request { get; set; } = null!;
        public virtual Employee Reviewer { get; set; } = null!;
    }
}
