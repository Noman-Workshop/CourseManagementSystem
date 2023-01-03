using CourseManagementSystem.Areas.Budgets.Repositories;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Services.Budgets.Repositories;

namespace CourseManagementSystem.Areas.Budgets.UnitOfWorks;

public abstract class IBudgetUnitOfWork : UnitOfWork {
	public IBudgetRepository BudgetRepository { get; }
	public IBudgetAuditLogRepository BudgetAuditLogRepository { get; }

	protected IBudgetUnitOfWork(
		DbContext context,
		IBudgetRepository budgetRepository,
		IBudgetAuditLogRepository budgetAuditLogRepository
	) : base(context) {
		BudgetRepository = budgetRepository;
		BudgetAuditLogRepository = budgetAuditLogRepository;
	}
}