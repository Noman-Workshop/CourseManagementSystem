using Microsoft.EntityFrameworkCore;
using Services.Common;

namespace Services.Leaves.LeaveTypes;

public abstract class ILeaveTypeRepository : Repository<Models.LeaveType, Guid> {
	protected ILeaveTypeRepository(DbContext context) : base(context) {
	}
}
