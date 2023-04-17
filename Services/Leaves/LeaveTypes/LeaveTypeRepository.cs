using Microsoft.EntityFrameworkCore;

namespace Services.Leaves.LeaveTypes;

class LeaveTypeRepository : ILeaveTypeRepository {
	public LeaveTypeRepository(DbContext context) : base(context) {
	}
}
