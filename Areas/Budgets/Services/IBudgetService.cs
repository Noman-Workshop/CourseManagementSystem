using CourseManagementSystem.Areas.Budgets.Controllers;
using CourseManagementSystem.Areas.Budgets.Dto;
using CourseManagementSystem.Areas.Budgets.Models;
using CourseManagementSystem.Data;
using CourseManagementSystem.Models.Table;

namespace CourseManagementSystem.Areas.Budgets.Services;

public interface IBudgetService : IService<Budget, string> {
	public Task<PagedResponse<Budget>> Find(JqueryDatatableParam param);

	// Create a service for downloading the budget in Excel format
	public Task<MemoryStream> ExportAsExcel();

	public Task Update(IEnumerable<BudgetUpdateDto> budgetUpdate, string userEmail);
	Task UploadBudgets(Stream stream);
}
