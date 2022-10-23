using CourseManagementSystem.Areas.Students.Models;
using CourseManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Students.Controllers; 

[Area("Students")]
public class HomeController : Controller {
	private readonly CMSDbContext _context;

	public HomeController(CMSDbContext context) {
		_context = context;
	}

	// GET: Students/Home
	public async Task<IActionResult> Index() => View(await _context.Students.ToListAsync());

	// GET: Students/Home/Details/5
	public async Task<IActionResult> Details(string id) {
		Student? student = await _context.Students
								.FirstOrDefaultAsync(m => m.Id == id);
		if (student == null) {
			return NotFound();
		}

		return View(student);
	}

	// GET: Students/Home/Create
	public IActionResult Create() => View();

	// POST: Students/Home/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,Name,Email")] Student student) {
		if (ModelState.IsValid) {
			_context.Add(student);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		return View(student);
	}

	// GET: Students/Home/Edit/5
	public async Task<IActionResult> Edit(string id) {
		Student? student = await _context.Students.FindAsync(id);
		if (student == null) {
			return NotFound();
		}

		return View(student);
	}

	// POST: Students/Home/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Email")] Student student) {
		if (id != student.Id) {
			return NotFound();
		}

		if (ModelState.IsValid) {
			try {
				_context.Update(student);
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!StudentExists(student.Id)) {
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
		Student? student = await _context.Students
								.FirstOrDefaultAsync(m => m.Id == id);
		if (student == null) {
			return NotFound();
		}

		return View(student);
	}

	// POST: Students/Home/Delete/5
	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(string id) {
		Student? student = await _context.Students.FindAsync(id);
		if (student != null) {
			_context.Students.Remove(student);
		}

		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool StudentExists(string id) => _context.Students.Any(e => e.Id == id);
}
