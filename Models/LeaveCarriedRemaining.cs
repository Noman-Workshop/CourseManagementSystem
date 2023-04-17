using System;
using System.Collections.Generic;

namespace Models {
	public partial class LeaveCarriedRemaining {
		public Guid EmployeeId { get; set; }
		public string LeaveTypeShortName { get; set; } = null!;
		public int CarriedOver { get; set; }
		public int LastAddedDays { get; set; }
		public DateTime LastAddedDate { get; set; }
		public int RemainingDays { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public virtual Employee Employee { get; set; } = null!;
	}
}
