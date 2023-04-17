namespace Services.Calender;

public class CalendarService : ICalendarService {
	private readonly ICalenderRepository _calenderRepository;

	public CalendarService(ICalenderRepository calenderRepository) {
		_calenderRepository = calenderRepository;
	}

	public async Task<List<Models.Calender>> GetCalenderRange(DateTime start, DateTime end) {
		var days = await _calenderRepository.Find(calender => calender.Day >= start && calender.Day <= end, "");
		return days;
	}

	public async Task SetCalenderDayType(DateTime date, string type) {
		_calenderRepository.Update(new Models.Calender { Day = date, Type = type, UpdatedAt = DateTime.Now });
		await _calenderRepository.CommitAsync();
	}
}
