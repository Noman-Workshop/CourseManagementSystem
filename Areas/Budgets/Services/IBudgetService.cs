using CourseManagementSystem.Areas.Budgets.Models;
using CourseManagementSystem.Data;
using CourseManagementSystem.Models.Table;

namespace CourseManagementSystem.Areas.Budgets.Services;

public interface IBudgetService : IService<Budget, string> {
	public Task<PagedResponse<Budget>> Find(JqueryDatatableParam param);
}
