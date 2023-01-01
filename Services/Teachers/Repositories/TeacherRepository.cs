using Microsoft.EntityFrameworkCore;

namespace Services.Teachers.Repositories;

public class TeacherRepository : ITeacherRepository {
	public TeacherRepository(DbContext context) : base(context) {
	}
}
