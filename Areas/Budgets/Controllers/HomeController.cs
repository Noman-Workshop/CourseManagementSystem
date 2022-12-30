using System.Security.Claims;
using CourseManagementSystem.Areas.Budgets.Dto;
using CourseManagementSystem.Areas.Budgets.Services;
using CourseManagementSystem.Models.Table;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.Areas.Budgets.Controllers {
	[Authorize]
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
				aaData = teachers.data,
				canEdit = User.IsInRole("Teacher")
			};

			return Json(result);
		}

		// GET: /budgets/Update
		[Route("Budgets/Update")]
		[HttpPost]
		public async Task Update([FromBody] IEnumerable<BudgetUpdateDto> budgetsUpdates) {
			// get the current authenticated user
			var user = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
			await _budgetService.Update(budgetsUpdates, user);

		}

		[Route("Budgets/ExportAsExcel")]
		public async Task<IActionResult> ExportAsExcel() {
			var stream = await _budgetService.ExportAsExcel();
			return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				"Budgets.xlsx");
		}

		public ViewResult Upload() => View();

		[Route("Budgets/UploadBudgets")]
		[HttpPost]
		public async Task<IActionResult> UploadBudgets(BudgetUploadDto budgetUploadDto) {
			// validate the model
			if (!ModelState.IsValid) {
				return View("Upload");
			}

			var user = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;

			var stream = budgetUploadDto.File.OpenReadStream();
			await _budgetService.UploadBudgets(stream, user, budgetUploadDto.EditDeadline);
			return View(nameof(Index));
		}
	}
}
