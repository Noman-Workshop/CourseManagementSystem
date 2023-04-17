using Microsoft.EntityFrameworkCore;

namespace Services.Employees;

class EmployeeRepository : IEmployeeRepository {
	public EmployeeRepository(DbContext context) : base(context) {
	}
}
