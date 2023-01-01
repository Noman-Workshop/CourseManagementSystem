using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Teachers.Repositories;

public abstract class ITeacherRepository : Repository<Teacher, string> {
	protected ITeacherRepository(DbContext context) : base(context) {
	}
}
