using Microsoft.EntityFrameworkCore;
using Services.Common;

namespace Services.Leaves.LeaveRequests;

public abstract class ILeaveRequestRepository : Repository<Models.LeaveRequest, Guid> {
	protected ILeaveRequestRepository(DbContext context) : base(context) {
	}
}