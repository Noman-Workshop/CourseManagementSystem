using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Students.Services;

namespace CourseManagementSystem.Areas.Students.Controllers;

[Area("Students")]
public class HomeController : Controller {
	private readonly IStudentService _studentService;

	public HomeController(IStudentService studentService) {
		_studentService = studentService;
	}

	// GET: Students/Home
	public async Task<IActionResult> Index() => View(await _studentService.Find());

	// GET: Students/Home/Details/5
	public async Task<IActionResult> Details(string id) {
		List<Student> students = await _studentService.Find(student => student.Id == id, "Address");

		if (students.Count == 0) {
			throw new ArgumentException();
		}

		return View(students[0]);
	}

	// GET: Students/Home/Create
	public IActionResult Create() => View();

	// POST: Students/Home/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(
		[Bind("Id,Name,Email")] Student student,
		[Bind("ZipCode,Street,House")] Address address
	) {
		student.Address = address;
		if (ModelState.IsValid) {
			await _studentService.Add(student);
			return RedirectToAction(nameof(Index));
		}

		return View(student);
	}

	// GET: Students/Home/Edit/5
	public async Task<IActionResult> Edit(string id) {
		List<Student> students = await _studentService.Find(student => student.Id == id, "Address");
		if (students.Count == 0) {
			return NotFound();
		}

		return View(students[0]);
	}

	// POST: Students/Home/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(
		string id,
		[Bind("Id,Name,Email")] Student student,
		[Bind(Prefix = "Address")] Address address
	) {
		if (id != student.Id) {
			return NotFound();
		}

		student.Address = address;
		if (ModelState.IsValid) {
			try {
				await _studentService.Update(student);
			} catch (DbUpdateConcurrencyException) {
				if (!await _studentService.Exists(student.Id)) {
					return NotFound();
				}

				throw;
			}

			return RedirectToAction(nameof(Index));
		}

		return View(student);
	}

	// GET: Students/Home/Delete/5
	public async Task<IActionResult> Delete(string id) {
		List<Student> students = await _studentService.Find(student => student.Id == id, "Address");
		if (students.Count == 0) {
			return NotFound();
		}

		return View(students[0]);
	}

	// POST: Students/Home/Delete/5
	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(string id) {
		Student student = (await _studentService.Find(student => student.Id == id, "Address"))[0];
		await _studentService.Delete(student);
		return RedirectToAction(nameof(Index));
	}
}
