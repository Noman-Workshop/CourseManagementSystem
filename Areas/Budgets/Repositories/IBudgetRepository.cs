using CourseManagementSystem.Areas.Budgets.Models;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Budgets.Repositories;

public abstract class IBudgetRepository: Repository<Budget, string> {
	protected IBudgetRepository(DbContext context) : base(context) {
	}
}
