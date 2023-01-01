using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CourseManagementSystem.Areas.Budgets.Repositories;

public abstract class IBudgetAuditLogRepository : Repository<BudgetAuditLog, string> {
	protected IBudgetAuditLogRepository(DbContext context) : base(context) {
	}
}
