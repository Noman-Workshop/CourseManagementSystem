using CourseManagementSystem.Areas.Departments.Services;
using CourseManagementSystem.ViewTables;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Teachers.Services;

namespace CourseManagementSystem.Areas.Departments.Controllers;

[Area("Departments")]
public class HomeController : Controller {
	private readonly IDepartmentService _departmentService;
	private readonly ITeacherService _teacherService;

	public HomeController(IDepartmentService departmentService, ITeacherService teacherService) {
		_departmentService = departmentService;
		_teacherService = teacherService;
	}

	// GET: Departments/Department
	public async Task<IActionResult> Index() => View(await _departmentService.Find(department => true, "Head"));

	// GET: Departments/Department/Details/5
	public async Task<IActionResult> Details(string id, string name) {
		// find department with composite key id and name
		Department department = await _departmentService.Find(id, name);
		return View(department);
	}

	// GET: Departments/Department/Create
	public IActionResult Create() {
		return View();
	}

	// POST: Departments/Department/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,Name,Head")] Department department) {
		if (!ModelState.IsValid) {
			return View(department);
		}

		try {
			Teacher head = (await _teacherService.Find((teacher => teacher.Id == department.Head.Id), "Address"))[0];
			department.Head = head;
			await _departmentService.Add(department);
			return RedirectToAction(nameof(Index));
		} catch (Exception) {
			ModelState["Name"]?.Errors.Add('"' + department.Name + '"' + " already exists");
			return View(department);
		}
	}

	// GET: Departments/Department/Edit/5
	public async Task<IActionResult> Edit(string id, string name) {
		Department department = await _departmentService.Find(id, name);
		return View(department);
	}

	// POST: Departments/Department/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Head")] Department department) {
		if (ModelState.IsValid) {
			await _departmentService.Update(department);
		}

		return View(department);
	}

	// GET: Departments/Department/Delete/5
	public async Task<IActionResult> Delete(string id, string name) {
		Department department = await _departmentService.Find(id, name);
		return View(department);
	}

	// POST: Departments/Department/Delete/5
	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(string id, string name) {
		Department department = await _departmentService.Find(id, name);
		await _departmentService.Delete(department);
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> SearchTeachers(string term) {
		PagedResponse<Teacher> teachers = await _teacherService.Find(new JqueryDatatableParam {
			sSearch = term,
			iDisplayLength = 10
		});
		return Json(teachers.data);
	}
}
