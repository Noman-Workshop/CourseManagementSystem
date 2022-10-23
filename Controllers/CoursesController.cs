using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseManagementSystem.Data;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Controllers {
	public class CoursesController : Controller {
		private readonly CMSDbContext _context;

		public CoursesController(CMSDbContext context) {
			_context = context;
		}

		// GET: Courses
		public async Task<IActionResult> Index() {
			return View(await _context.Course.ToListAsync());
		}

		// GET: Courses/Details/5
		public async Task<IActionResult> Details(string id) {
			var course = await _context.Course
							.FirstOrDefaultAsync(m => m.Id == id);
			if (course == null) {
				return NotFound();
			}

			return View(course);
		}

		// GET: Courses/Create
		public IActionResult Create() {
			return View();
		}

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
			var course = await _context.Course.FindAsync(id);
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
			var course = await _context.Course
							.FirstOrDefaultAsync(m => m.Id == id);
			if (course == null) {
				return NotFound();
			}

			return View(course);
		}

		// POST: Courses/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id) {
			var course = await _context.Course.FindAsync(id);
			if (course != null) {
				_context.Course.Remove(course);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool CourseExists(string id) {
			return _context.Course.Any(e => e.Id == id);
		}
	}
}
