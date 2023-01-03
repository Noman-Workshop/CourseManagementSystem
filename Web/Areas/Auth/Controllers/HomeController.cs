using DTOs.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Auth.Services;

namespace CourseManagementSystem.Areas.Auth.Controllers;

[Area("Auth")]
public class HomeController : Controller {
	private readonly IAuthService _authService;

	public HomeController(IAuthService authService) {
		_authService = authService;
	}

	public ActionResult Index() {
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Index(LoginDto loginDto) {
		if (!ModelState.IsValid) {
			return View();
		}

		try {
			var principal = _authService.SignIn(loginDto);
			await HttpContext.SignInAsync(AuthTypes.UsernamePasswordCookies.ToString(), principal);
			return RedirectToAction("Index", "Home", new { area = "Teachers" });
		} catch (ArgumentException) {
			ModelState.AddModelError("UserEmail", "Invalid user email");
			ModelState.AddModelError("Password", "Invalid password");
			return View();
		}
	}

	[HttpPost]
	public async Task<IActionResult> Logout() {
		await HttpContext.SignOutAsync(AuthTypes.UsernamePasswordCookies.ToString());
		return RedirectToAction("Index", "Home", new { area = "Auth" });
	}
}