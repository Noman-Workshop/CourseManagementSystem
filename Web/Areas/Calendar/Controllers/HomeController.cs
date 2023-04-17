using DTOs.Calender;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Calender;
using Services.Mappers;

namespace CourseManagementSystem.Areas.Calendar.Controllers;

[Area("Calendar")]
public class HomeController : Controller {
	private readonly ICalendarService _calendarService;
	private readonly IMapperService _mapper;

	public HomeController(ICalendarService calendarService, IMapperService mapper) {
		_calendarService = calendarService;
		_mapper = mapper;
	}

	public IActionResult Index() {
		return View();
	}

	[HttpPost]
	public async Task AddEvent([FromForm] DateTime date, [FromForm(Name = "day-type")] string type) {
		await _calendarService.SetCalenderDayType(date, type);
	}

	public async Task<IActionResult> GetDayTypeRange(
		[FromQuery] DateTime start,
		[FromQuery] DateTime end
	) {
		var calenderRange = await _calendarService.GetCalenderRange(start, end);
		var calendarDays = calenderRange.Select(day => _mapper.Map<Calender, CalendarDayDto>(day)).ToList();
		return Ok(new {
			data = calendarDays
		});
	}
}
