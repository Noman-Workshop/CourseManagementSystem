using System.Linq.Expressions;
using CourseManagementSystem.Areas.Budgets.Models;
using CourseManagementSystem.Areas.Budgets.Repositories;
using CourseManagementSystem.Models.Table;

namespace CourseManagementSystem.Areas.Budgets.Services;

public class BudgetService : IBudgetService {
	private readonly IBudgetRepository _budgetRepository;

	public BudgetService(IBudgetRepository budgetRepository) {
		_budgetRepository = budgetRepository;
	}

	public async Task<List<Budget>> Find() {
		return await _budgetRepository.Find(budget => true, "AuditsLogs,Department");
	}

	public async Task<PagedResponse<Budget>> Find(JqueryDatatableParam param) {
		List<Budget> teachers = await Find();
		return new PagedResponse<Budget> {
			data = teachers,
		};
	}

	public ValueTask<Budget> Find(string id) => throw new NotImplementedException();

	public Task<List<Budget>> Find(Expression<Func<Budget, bool>> condition, string includeAttributes) =>
		throw new NotImplementedException();

	public Task Add(Budget entity) => throw new NotImplementedException();

	public Task Update(Budget entity) => throw new NotImplementedException();

	public Task Delete(Budget entity) => throw new NotImplementedException();

	public Task<bool> Exists(string id) => throw new NotImplementedException();
}
