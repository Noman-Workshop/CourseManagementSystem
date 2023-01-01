using Microsoft.EntityFrameworkCore;

namespace Services.Students.Repositories;

public class StudentRepository : IStudentRepository {
	public StudentRepository(DbContext context) : base(context) {
	}
}
