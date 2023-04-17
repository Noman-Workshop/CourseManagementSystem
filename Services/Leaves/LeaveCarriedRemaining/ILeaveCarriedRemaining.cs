using Microsoft.EntityFrameworkCore;
using Services.Common;

namespace Services.Leaves.LeaveCarriedRemaining;

public abstract class ILeaveCarriedRemaining : Repository<Models.LeaveCarriedRemaining, ValueTuple<Guid, string>> {
	protected ILeaveCarriedRemaining(DbContext context) : base(context) {
	}
}