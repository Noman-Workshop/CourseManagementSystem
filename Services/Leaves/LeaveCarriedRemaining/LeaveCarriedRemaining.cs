using Microsoft.EntityFrameworkCore;

namespace Services.Leaves.LeaveCarriedRemaining;

class LeaveCarriedRemaining : ILeaveCarriedRemaining {
	public LeaveCarriedRemaining(DbContext context) : base(context) {
	}
}
