using CourseManagementSystem.Areas.Departments.Models;
using CourseManagementSystem.Areas.Departments.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.Areas.Departments.Controllers;

[Area("Departments")]
public class HomeController : Controller {
	private readonly IDepartmentService _departmentService;

	public HomeController(IDepartmentService departmentService) {
		_departmentService = departmentService;
	}

	// GET: Departments/Department
	public async Task<IActionResult> Index() => View(await _departmentService.Find());

	// GET: Departments/Department/Details/5
	public async Task<IActionResult> Details(string id) {
		Department department = await _departmentService.Find(id);
		return View(department);
	}

	// GET: Departments/Department/Create
	public IActionResult Create() {
		return View();
	}

	// POST: Departments/Department/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,Name")] Department department) {
		if (ModelState.IsValid) {
			await _departmentService.Add(department);
			return RedirectToAction(nameof(Index));
		}

		return View(department);
	}

	// GET: Departments/Department/Edit/5
	public async Task<IActionResult> Edit(string id) {
		Department department = await _departmentService.Find(id);
		return View(department);
	}

	// POST: Departments/Department/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] Department department) {
		if (ModelState.IsValid) {
			await _departmentService.Update(department);
		}

		return View(department);
	}

	// GET: Departments/Department/Delete/5
	public async Task<IActionResult> Delete(string id) {
		Department department = await _departmentService.Find(id);
		return View(department);
	}

	// POST: Departments/Department/Delete/5
	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(string id) {
		Department department = await _departmentService.Find(id);
		await _departmentService.Delete(department);
		return RedirectToAction(nameof(Index));
	}
}
