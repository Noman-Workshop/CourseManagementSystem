using Microsoft.EntityFrameworkCore;

namespace Services.Leaves.LeaveRequests;

public class LeaveRequestRepository : ILeaveRequestRepository {
	public LeaveRequestRepository(DbContext context) : base(context) {
	}
}
