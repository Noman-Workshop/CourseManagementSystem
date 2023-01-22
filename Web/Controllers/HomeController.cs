﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace CourseManagementSystem.Controllers;

public class HomeController : Controller {
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger) {
		_logger = logger;
	}

	public IActionResult Index() => View();

	public IActionResult Privacy() => View();

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error() => View(new ErrorDto
		{ RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

}
