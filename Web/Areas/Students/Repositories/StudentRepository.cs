using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Students.Repositories;

public class StudentRepository : IStudentRepository {
	public StudentRepository(DbContext context) : base(context) {
	}
}
