using CourseManagementSystem.Areas.Budgets.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Budgets.Repositories;

namespace CourseManagementSystem.Areas.Budgets.UnitOfWorks;

public class BudgetUnitOfWork : IBudgetUnitOfWork {
	public BudgetUnitOfWork(
		DbContext context,
		IBudgetRepository budgetRepository,
		IBudgetAuditLogRepository budgetAuditLogRepository
	) : base(context, budgetRepository, budgetAuditLogRepository) {
	}
}
