using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Students.Repositories;

public abstract class IStudentRepository : Repository<Student, string> {
	protected IStudentRepository(DbContext context) : base(context) {
	}
}
