using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Departments.Repositories;

public class DepartmentRepository : IDepartmentRepository {
	public DepartmentRepository(DbContext context) : base(context) {
	}
}
