using Microsoft.EntityFrameworkCore;

class CalenderRepository : ICalenderRepository {
	public CalenderRepository(DbContext context) : base(context) {
	}
}
