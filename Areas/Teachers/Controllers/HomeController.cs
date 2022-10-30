using CourseManagementSystem.Areas.Addresses.Models;
using CourseManagementSystem.Areas.Teachers.Models;
using CourseManagementSystem.Areas.Teachers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Teachers.Controllers;

[Area("Teachers")]
public class HomeController : Controller {
	private readonly ITeacherService _teacherService;

	public HomeController(ITeacherService teacherService) {
		_teacherService = teacherService;
	}

	// GET: Teachers/Home
	public async Task<IActionResult> Index() => View(await _teacherService.Find());

	// GET: Teachers/Home/Details/5
	public async Task<IActionResult> Details(string id) {
		List<Teacher> teachers = await _teacherService.Find(teacher => teacher.Id == id, "Address");

		if (teachers.Count == 0) {
			throw new ArgumentException();
		}

		return View(teachers[0]);
	}

	// GET: Teachers/Home/Create
	public IActionResult Create() => View();

	// POST: Teachers/Home/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(
		[Bind("Id,Name,Email")] Teacher teacher,
		[Bind("ZipCode,Street,House")] Address address
	) {
		teacher.Address = address;
		if (ModelState.IsValid) {
			await _teacherService.Add(teacher);
			return RedirectToAction(nameof(Index));
		}

		return View(teacher);
	}

	// GET: Teachers/Home/Edit/5
	public async Task<IActionResult> Edit(string id) {
		List<Teacher> teachers = await _teacherService.Find(teacher => teacher.Id == id, "Address");
		if (teachers.Count == 0) {
			return NotFound();
		}

		return View(teachers[0]);
	}

	// POST: Teachers/Home/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(
		string id,
		[Bind("Id,Name,Email")] Teacher teacher,
		[Bind(Prefix = "Address")] Address address
	) {
		if (id != teacher.Id) {
			return NotFound();
		}

		teacher.Address = address;
		if (ModelState.IsValid) {
			try {
				await _teacherService.Update(teacher);
			} catch (DbUpdateConcurrencyException) {
				if (!await _teacherService.Exists(teacher.Id)) {
					return NotFound();
				}

				throw;
			}

			return RedirectToAction(nameof(Index));
		}

		return View(teacher);
	}

	// GET: Teachers/Home/Delete/5
	public async Task<IActionResult> Delete(string id) {
		List<Teacher> teachers = await _teacherService.Find(teacher => teacher.Id == id, "Address");
		if (teachers.Count == 0) {
			return NotFound();
		}

		return View(teachers[0]);
	}

	// POST: Teachers/Home/Delete/5
	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(string id) {
		Teacher teacher = (await _teacherService.Find(teacher => teacher.Id == id, "Address"))[0];
		await _teacherService.Delete(teacher);
		return RedirectToAction(nameof(Index));
	}
}
