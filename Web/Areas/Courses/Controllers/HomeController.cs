using CourseManagementSystem.Areas.Courses.Models;
using CourseManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Courses.Controllers;

[Area("Courses")]
public class HomeController : Controller {
	private readonly CMSDbContext _context;

	public HomeController(CMSDbContext context) {
		_context = context;
	}

	// GET: Courses
	public async Task<IActionResult> Index() => View(await _context.Courses.ToListAsync());

	// GET: Courses/Details/5
	public async Task<IActionResult> Details(string id) {
		Course? course = await _context.Courses
							.FirstOrDefaultAsync(m => m.Id == id);
		if (course == null) {
			return NotFound();
		}

		return View(course);
	}

	// GET: Courses/Create
	public IActionResult Create() => View();

	// POST: Courses/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,Name,Description,Credits")] Course course) {
		if (ModelState.IsValid) {
			_context.Add(course);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		return View(course);
	}

	// GET: Courses/Edit/5
	public async Task<IActionResult> Edit(string id) {
		Course? course = await _context.Courses.FindAsync(id);
		if (course == null) {
			return NotFound();
		}

		return View(course);
	}

	// POST: Courses/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Description,Credits")] Course course) {
		if (id != course.Id) {
			return NotFound();
		}

		if (ModelState.IsValid) {
			try {
				_context.Update(course);
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!CourseExists(course.Id)) {
					return NotFound();
				}

				throw;
			}

			return RedirectToAction(nameof(Index));
		}

		return View(course);
	}

	// GET: Courses/Delete/5
	public async Task<IActionResult> Delete(string id) {
		Course? course = await _context.Courses
							.FirstOrDefaultAsync(m => m.Id == id);
		if (course == null) {
			return NotFound();
		}

		return View(course);
	}

	// POST: Courses/Delete/5
	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(string id) {
		Course? course = await _context.Courses.FindAsync(id);
		if (course != null) {
			_context.Courses.Remove(course);
		}

		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool CourseExists(string id) => _context.Courses.Any(e => e.Id == id);
}
