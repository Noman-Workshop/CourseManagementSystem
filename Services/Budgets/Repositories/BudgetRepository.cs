using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Budgets.Repositories;

public class BudgetRepository : IBudgetRepository {
	public BudgetRepository(DbContext context) : base(context) {
	}
}
