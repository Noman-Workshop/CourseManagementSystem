using System.Security.Claims;
using CourseManagementSystem.Extensions;
using DTOs.Login;
using DTOs.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Models;
using Models.Constants;
using Services.Auth;
using Services.Mappers;

namespace CourseManagementSystem.Areas.Auth.Controllers;

[Area("Auth")]
public class HomeController : Controller {
	private readonly IAuthService _authService;
	private readonly IMapperService _mapperService;
	private readonly IDistributedCache _cache;

	public HomeController(IAuthService authService, IMapperService mapperService, IDistributedCache cache) {
		_authService = authService;
		_mapperService = mapperService;
		_cache = cache;
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
			var userWithPrincipal = _authService.SignIn(loginDto);
			var userDto = _mapperService.Map<User, UserDto>((User) userWithPrincipal[0]);
			await _cache.Set(userDto.Email, userDto);
			await HttpContext.SignInAsync(AuthTypes.UsernamePasswordCookies.ToString(),
				(ClaimsPrincipal) userWithPrincipal[1]);
			return RedirectToAction("Index", "Home");
		} catch (ArgumentException) {
			ModelState.AddModelError("UserEmail", "Invalid user email");
			ModelState.AddModelError("Password", "Invalid password");
			return View();
		}
	}

	[HttpGet]
	public async Task<IActionResult> Logout() {
		await HttpContext.SignOutAsync(AuthTypes.UsernamePasswordCookies.ToString());
		return RedirectToAction("Index", "Home", new { area = "Auth" });
	}
}
