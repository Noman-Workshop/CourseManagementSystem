using Microsoft.EntityFrameworkCore;
using Services.Budgets.Repositories;

namespace CourseManagementSystem.Areas.Budgets.Repositories;

public class BudgetAuditLogRepository : IBudgetAuditLogRepository {
	public BudgetAuditLogRepository(DbContext context) : base(context) {
	}
}
