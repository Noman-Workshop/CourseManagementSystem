using System.Text.Json.Serialization;

namespace DTOs.Calender;

public class CalendarDayDto {
	public DateTime Day { get; set; }
	public string Type { get; set; }
}
