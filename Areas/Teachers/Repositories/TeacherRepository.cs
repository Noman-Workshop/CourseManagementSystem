using CourseManagementSystem.Areas.Teachers.Models;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Teachers.Repositories;

public class TeacherRepository : ITeacherRepository {
	public TeacherRepository(DbContext context) : base(context) {
	}
}
