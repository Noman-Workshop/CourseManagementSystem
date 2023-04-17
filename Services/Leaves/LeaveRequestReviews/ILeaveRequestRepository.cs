using Microsoft.EntityFrameworkCore;
using Models;
using Services.Common;

namespace Services.Leaves.LeaveRequestReviews;

public abstract class ILeaveRequestReviewRepository : Repository<LeaveRequestReview, Guid> {
	protected ILeaveRequestReviewRepository(DbContext context) : base(context) {
	}
}