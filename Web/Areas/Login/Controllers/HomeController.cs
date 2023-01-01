using CourseManagementSystem.Areas.Auth.Models;
using CourseManagementSystem.Areas.Auth.Services;
using CourseManagementSystem.Areas.Login.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.Areas.Login.Controllers {
	[Area("Login")]
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
				ModelState.AddModelError("Credential", "Invalid username or password");
				return View();
			}
		}
	}
}
