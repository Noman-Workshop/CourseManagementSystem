using CourseManagementSystem.Areas.Departments.Models;
using CourseManagementSystem.Data;

namespace CourseManagementSystem.Areas.Departments.Services;

public interface IDepartmentService : IService<Department, string> {
	// find department by composite key id and name
	Task<Department> Find(string id, string name);
}
