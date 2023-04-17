using Microsoft.EntityFrameworkCore;

namespace Services.Leaves.LeaveRequestReviews;

public class LeaveRequestReviewRepository : ILeaveRequestReviewRepository {
	public LeaveRequestReviewRepository(DbContext context) : base(context) {
	}
}
