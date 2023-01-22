using System;
using System.Collections.Generic;

namespace Models
{
    public partial class LeaveType
    {
        public LeaveType()
        {
            LeaveRequests = new HashSet<LeaveRequest>();
        }

        public Guid Id { get; set; }
        public string ShortName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string GenderConstraint { get; set; } = null!;
        public int MaxDaysInOneGo { get; set; }
        public int NoOfTimesRedeemable { get; set; }
        public int MaxDaysBeforeBalanceReset { get; set; }
        public bool IsBalanceForwarded { get; set; }
        public bool IsRealTimeBalanced { get; set; }
        public bool IsPrePostHolidayRestricted { get; set; }
        public bool IsProbationPeriodRestricted { get; set; }
        public bool IsWeekendIncluded { get; set; }
        public bool IsHolidayIncluded { get; set; }
        public bool CanBePartiallyAllocated { get; set; }
        public bool IsLateAdjusted { get; set; }
        public bool IsAbsentAdjusted { get; set; }
        public bool IsPaid { get; set; }
        public int MinServiceDays { get; set; }
        public int MinEmployeeLevelRank { get; set; }
        public bool CanBeCashed { get; set; }
        public int ReviewForwardDepth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int NoOfDaysAllocatedPerYear { get; set; }

        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
