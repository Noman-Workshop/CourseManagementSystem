using CourseManagementSystem.Areas.Budgets.Models;
using CourseManagementSystem.Areas.Budgets.Services;
using CourseManagementSystem.Models.Table;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.Areas.Budgets.Controllers {
	[Area("Budgets")]
	public class HomeController : Controller {
		private readonly IBudgetService _budgetService;

		public HomeController(IBudgetService budgetService) {
			_budgetService = budgetService;
		}

		// GET: /budgets
		public IActionResult Index() {
			return View();
		}

		// GET: GetTableData
		[Route("Budgets/GetTableData")]
		public async Task<IActionResult> GetTableData(JqueryDatatableParam param) {
			var teachers = await _budgetService.Find(param);
			var result = new {
				aaData = teachers.data
			};

			return Json(result);
		}

		// GET: /budgets/Update
		[Route("Budgets/Update")]
		public void Update([FromBody] BudgetUpdateDto[] budgetUpdates) {
		}
	}

	public class BudgetUpdateDto {
		public int Id { get; set; }
		public decimal Amount { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
