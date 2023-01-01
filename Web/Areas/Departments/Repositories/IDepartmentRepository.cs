using CourseManagementSystem.Areas.Departments.Models;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Departments.Repositories;

public abstract class IDepartmentRepository : Repository<Department, string> {
	protected IDepartmentRepository(DbContext context) : base(context) {
	}
}
