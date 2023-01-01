using CourseManagementSystem.Areas.Budgets.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Budgets.UnitOfWorks;

class BudgetUnitOfWork : IBudgetUnitOfWork {
	public BudgetUnitOfWork(
		DbContext context,
		IBudgetRepository budgetRepository,
		IBudgetAuditLogRepository budgetAuditLogRepository
	) : base(context, budgetRepository, budgetAuditLogRepository) {
	}
}
