using CourseManagementSystem.Areas.Teachers.Models;
using CourseManagementSystem.Data;
using CourseManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseManagementSystem.Areas.Addresses.Services;

namespace CourseManagementSystem.Areas.Teachers.Controllers;

[Area("Teachers")]
public class HomeController : Controller {
	private readonly CMSDbContext _context;
	private readonly IAddressServices _addressesService;

	public HomeController(CMSDbContext context, IAddressServices addressesService) {
		_context = context;
		_addressesService = addressesService;
	}

	// GET: Teachers/Home
	public async Task<IActionResult> Index() => View(await _context.Teachers.ToListAsync());

	// GET: Teachers/Home/Details/5
	public async Task<IActionResult> Details(string id) {
		Teacher? teacher = await _context.Teachers
								.Include(teacher => teacher.Address)
								.FirstOrDefaultAsync(m => m.Id == id);

		if (teacher == null) {
			return NotFound();
		}

		return View(teacher);
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
		address = await _addressesService.Create(address);
		teacher.Address = address;
		if (ModelState.IsValid) {
			_context.Add(teacher);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		return View(teacher);
	}

	// GET: Teachers/Home/Edit/5
	public async Task<IActionResult> Edit(string id) {
		Teacher? teacher = await _context.Teachers
								.Include(teacher => teacher.Address)
								.FirstOrDefaultAsync(teacher => id == teacher.Id);
		if (teacher == null) {
			return NotFound();
		}

		return View(teacher);
	}

	// POST: Teachers/Home/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(
		string id,
		[Bind("Id,Name,Email")] Teacher teacher,
		[Bind("ZipCode,Street,House")] Address address
	) {
		if (id != teacher.Id) {
			return NotFound();
		}

		if (ModelState.IsValid) {
			try {
				_context.Update(teacher);
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!TeacherExists(teacher.Id)) {
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
		Teacher? teacher = await _context.Teachers
								.FirstOrDefaultAsync(m => m.Id == id);
		if (teacher == null) {
			return NotFound();
		}

		return View(teacher);
	}

	// POST: Teachers/Home/Delete/5
	[HttpPost]
	[ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(string id) {
		Teacher? teacher = await _context.Teachers.FindAsync(id);
		if (teacher != null) {
			_context.Teachers.Remove(teacher);
		}

		await _context.SaveChangesAsync();
		return RedirectToAction(nameof(Index));
	}

	private bool TeacherExists(string id) => _context.Teachers.Any(e => e.Id == id);
}
