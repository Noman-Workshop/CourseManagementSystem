using Microsoft.EntityFrameworkCore;
using Models;
using Services.Common;

namespace Services.Employees;

public abstract class IEmployeeRepository : Repository<Employee, Guid> {
	protected IEmployeeRepository(DbContext context) : base(context) {
	}
}