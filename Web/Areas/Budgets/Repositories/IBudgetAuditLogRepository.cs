using CourseManagementSystem.Areas.Budgets.Models;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Budgets.Repositories;

public abstract class IBudgetAuditLogRepository : Repository<BudgetAuditLog, string> {
	protected IBudgetAuditLogRepository(DbContext context) : base(context) {
	}
}
