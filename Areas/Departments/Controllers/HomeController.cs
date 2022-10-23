using CourseManagementSystem.Areas.Departments.Models;
using CourseManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Departments.Controllers; 

[Area("Departments")]
public class HomeController : Controller {
	private readonly CMSDbContext _context;

	public HomeController(CMSDbContext context) {
		_context = context;
	}

	// GET: Departments/Department
	public async Task<IActionResult> Index() => View(await _context.Departments.ToListAsync());

	// GET: Departments/Department/Details/5
	public async Task<IActionResult> Details(string id) {
		Department? department = await _context.Departments
									.FirstOrDefaultAsync(m => m.Id == id);
		if (department == null) {
			return NotFound();
		}

		return View(department);
	}

	// GET: Departments/Department/Create
	public IActionResult Create() => View();

	// POST: Departments/Department/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,Name")] Department department) {
		if (ModelState.IsValid) {
			_context.Add(department);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		return View(department);
	}

	// GET: Departments/Department/Edit/5
	public async Task<IActionResult> Edit(string id) {
		Department? department = await _context.Departments.FindAsync(id);
		if (department == null) {
			return NotFound();
		}

		return View(department);
	}

	// POST: Departments/Department/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] Department department) {
		if (id != department.Id) {
			return NotFound();
		}

		if (ModelState.IsValid) {
			try {
				_context.Update(department);
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!DepartmentExists(department.Id)) {
					return NotFound();
				}

				throw;
			}

			return RedirectToAction(nameof(Index));
		}

		return View(department);
	}

	// GET: Departments/Department/Delete/5
	public async Task<IActionResult> Delete(string id) {
		Department? department = await _context.Departments
									.FirstOrDefaultAsync(m => m.Id == id);
		if (department == null) {
			return NotFound();
		}

		return View(department);
	}

	// POST: Departments/Department/Delete/5
	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(string id) {
		Department? department = await _context.Departments.FindAsync(id);
		if (department != null) {
			_context.Departments.Remove(department);
		}

		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool DepartmentExists(string id) => _context.Departments.Any(e => e.Id == id);
}
