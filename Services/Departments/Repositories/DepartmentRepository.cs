using Microsoft.EntityFrameworkCore;

namespace Services.Departments.Repositories;

public class DepartmentRepository : IDepartmentRepository {
	public DepartmentRepository(DbContext context) : base(context) {
	}
}
