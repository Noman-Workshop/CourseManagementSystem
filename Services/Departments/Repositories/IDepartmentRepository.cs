using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Departments.Repositories;

public abstract class IDepartmentRepository : Repository<Department, string> {
	protected IDepartmentRepository(DbContext context) : base(context) {
	}
}
