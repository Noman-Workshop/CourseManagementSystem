namespace Services.Calender;

public interface ICalendarService {
	public Task<List<Models.Calender>> GetCalenderRange(DateTime start, DateTime end);

	public Task SetCalenderDayType(DateTime date, string type);
}