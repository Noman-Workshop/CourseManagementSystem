using CourseManagementSystem.Areas.Teachers.Models;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Teachers.Repository;

public class TeacherRepository : Repository<Teacher> {
	public TeacherRepository(DbContext context) : base(context) {
	}
}
