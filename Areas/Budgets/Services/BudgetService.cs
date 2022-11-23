using System.Collections;
using System.Data;
using System.Linq.Expressions;
using ClosedXML.Excel;
using CourseManagementSystem.Areas.Budgets.Dto;
using CourseManagementSystem.Areas.Budgets.Models;
using CourseManagementSystem.Areas.Budgets.UnitOfWorks;
using CourseManagementSystem.Areas.Departments.Models;
using CourseManagementSystem.Areas.Departments.Services;
using CourseManagementSystem.Areas.Users.Models;
using CourseManagementSystem.Areas.Users.Services;
using CourseManagementSystem.Models.Table;

namespace CourseManagementSystem.Areas.Budgets.Services;

public class BudgetService : IBudgetService {
	private readonly IUserService _userService;
	private readonly IBudgetUnitOfWork _budgetUnitOfWork;
	private readonly IDepartmentService _departmentService;

	public BudgetService(
		IUserService userService,
		IBudgetUnitOfWork budgetUnitOfWork,
		IDepartmentService departmentService
	) {
		_userService = userService;
		_budgetUnitOfWork = budgetUnitOfWork;
		_departmentService = departmentService;
	}

	public async Task<List<Budget>> Find() {
		return await _budgetUnitOfWork.BudgetRepository.Find(budget => true, "AuditsLogs,Department");
	}

	public async Task<PagedResponse<Budget>> Find(JqueryDatatableParam param) {
		List<Budget> teachers = await Find();
		return new PagedResponse<Budget> {
			data = teachers,
		};
	}

	public async Task<MemoryStream> ExportAsExcel() {
		// Create a new DataTable.
		DataTable table = new DataTable("Budgets");

		// Add columns to the DataTable.
		table.Columns.Add("Id", typeof(string));
		table.Columns.Add("Name", typeof(string));
		table.Columns.Add("Amount", typeof(decimal));
		table.Columns.Add("Department", typeof(string));

		// Add rows to the DataTable.
		List<Budget> budgets = await Find();
		foreach (Budget budget in budgets) {
			table.Rows.Add(
				budget.Id,
				budget.Name,
				budget.Amount,
				budget.Department.Name
			);
		}

		// Create a new XLWorkbook.
		XLWorkbook workbook = new XLWorkbook();
		// Add the DataTable as a worksheet.
		workbook.Worksheets.Add(table);
		// Create a MemoryStream.
		MemoryStream stream = new MemoryStream();
		// Save the workbook to the stream.
		workbook.SaveAs(stream);
		// Return the stream.
		return stream;
	}

	public async ValueTask<Budget> Find(string id) {
		return await _budgetUnitOfWork.BudgetRepository.Find(id) ?? throw new Exception("Budget not found");
	}

	public Task<List<Budget>> Find(Expression<Func<Budget, bool>> condition, string includeAttributes) =>
		_budgetUnitOfWork.BudgetRepository.Find(condition, includeAttributes);

	public Task Add(Budget entity) => throw new NotImplementedException();

	public async Task Update(Budget entity) {
		_budgetUnitOfWork.BudgetRepository.Update(entity);
		await _budgetUnitOfWork.CommitAsync();
	}

	public async Task Update(IEnumerable<BudgetUpdateDto> budgetUpdates, string userEmail) {
		User user = await _userService.Find(userEmail);
		foreach (BudgetUpdateDto budgetUpdateDto in budgetUpdates) {
			Budget budget = await Find(budgetUpdateDto.Id);
			budget.Amount = budgetUpdateDto.Amount;
			var budgetAuditLog = new BudgetAuditLog {
				CreatedAt = DateTime.Parse(budgetUpdateDto.Timestamp, null,
					System.Globalization.DateTimeStyles.RoundtripKind),
				CreatedBy = user,
				Budget = budget
			};
			_budgetUnitOfWork.BudgetAuditLogRepository.Add(budgetAuditLog);
			_budgetUnitOfWork.BudgetRepository.Update(budget);
		}

		await _budgetUnitOfWork.CommitAsync();
	}

	public async Task UploadBudgets(Stream stream, String userEmail) {
		User user = await _userService.Find(userEmail);
		// Convert the stream to excel workbook.
		XLWorkbook workbook = new XLWorkbook(stream);
		// Get the first worksheet.
		IXLWorksheet worksheet = workbook.Worksheet(1);
		// Create a new DataTable.
		DataTable table = new DataTable("Budgets");
		// Add columns to the DataTable.
		table.Columns.Add("Name", typeof(string));
		table.Columns.Add("Amount", typeof(double));
		table.Columns.Add("Department", typeof(string));
		// Add rows to the DataTable.
		bool firstRow = true;
		foreach (IXLRow row in worksheet.Rows()) {
			if (firstRow) {
				firstRow = false;
				continue;
			}

			if (row.Cell(1).Value.ToString() == "") {
				break;
			}

			table.Rows.Add(
				row.Cell(1).Value,
				row.Cell(2).Value,
				row.Cell(3).Value
			);
		}

		// Create a new Budgets.
		List<Budget> budgets = new List<Budget>();
		// Create a list of BudgetAuditLogs.
		List<BudgetAuditLog> budgetAuditLogs = new List<BudgetAuditLog>();
		// create a hashtable to store the department name and department id.
		Hashtable departments = new Hashtable();
		foreach (DataRow row in table.Rows) {
			// Get the department name.
			string departmentName = row["Department"].ToString();
			if (!departments.ContainsKey(departmentName)) {
				Department department = (await _departmentService.Find(d => d.Name == departmentName, ""))[0];
				departments.Add(departmentName, department);
			}

			var budget = new Budget {
				Name = row["Name"].ToString(),
				Amount = decimal.Parse(row["Amount"].ToString()),
				Department = departments[departmentName] as Department,
				Currency = "taka"
			};
			budgets.Add(budget);

			// add the budget audit log.
			var budgetAuditLog = new BudgetAuditLog {
				CreatedAt = DateTime.Now,
				CreatedBy = user,
				Budget = budget
			};
			budgetAuditLogs.Add(budgetAuditLog);
		}

		// Add the budgets to the database.
		_budgetUnitOfWork.BudgetRepository.AddRange(budgets);
		// Add the budget audit logs to the database.
		_budgetUnitOfWork.BudgetAuditLogRepository.AddRange(budgetAuditLogs);
		// Save the changes.
		await _budgetUnitOfWork.CommitAsync();
	}

	public Task Delete(Budget entity) => throw new NotImplementedException();

	public Task<bool> Exists(string id) => throw new NotImplementedException();
}
