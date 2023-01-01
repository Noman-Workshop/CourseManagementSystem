using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CourseManagementSystem.Areas.Budgets.Repositories;

public abstract class IBudgetRepository: Repository<Budget, string> {
	protected IBudgetRepository(DbContext context) : base(context) {
	}
}
