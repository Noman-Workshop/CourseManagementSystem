using CourseManagementSystem.Areas.Teachers.Models;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Teachers.Repositories;

public abstract class ITeacherRepository : Repository<Teacher, string> {
	protected ITeacherRepository(DbContext context) : base(context) {
	}
}
