using CourseManagementSystem.Data;
using CourseManagementSystem.ViewTables;
using DTOs.Budgets;
using Models;

namespace Services.Budgets.Services;

public interface IBudgetService : IService<Budget, string> {
	public Task<PagedResponse<Budget>> Find(JqueryDatatableParam param);

	// Create a service for downloading the budget in Excel format
	public Task<MemoryStream> ExportAsExcel();

	public Task Update(IEnumerable<BudgetUpdateDto> budgetUpdate, string userEmail);
	Task UploadBudgets(Stream stream, string userEmail, DateTime? editDeadline);
}
