using Microsoft.EntityFrameworkCore;
using Models;
using Services.Common;

public abstract class ICalenderRepository : Repository<Calender, DateTime> {
	protected ICalenderRepository(DbContext context) : base(context) {
	}
}