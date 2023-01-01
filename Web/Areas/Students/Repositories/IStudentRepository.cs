using CourseManagementSystem.Areas.Students.Models;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Students.Repositories;

public abstract class IStudentRepository : Repository<Student, string> {
	protected IStudentRepository(DbContext context) : base(context) {
	}
}
