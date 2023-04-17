using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Employee
    {
        public Employee()
        {
            InverseSupervisor = new HashSet<Employee>();
            LeaveCarriedRemainings = new HashSet<LeaveCarriedRemaining>();
            LeaveRequestReviewRelievers = new HashSet<LeaveRequestReview>();
            LeaveRequestReviewReviewers = new HashSet<LeaveRequestReview>();
            LeaveRequests = new HashSet<LeaveRequest>();
        }

        public Guid Id { get; set; }
        public string Level { get; set; } = null!;
        public Guid? SupervisorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? JoinDate { get; set; }

        public virtual User IdNavigation { get; set; } = null!;
        public virtual EmployeeLevel LevelNavigation { get; set; } = null!;
        public virtual Employee? Supervisor { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual ICollection<Employee> InverseSupervisor { get; set; }
        public virtual ICollection<LeaveCarriedRemaining> LeaveCarriedRemainings { get; set; }
        public virtual ICollection<LeaveRequestReview> LeaveRequestReviewRelievers { get; set; }
        public virtual ICollection<LeaveRequestReview> LeaveRequestReviewReviewers { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}
