using CourseManagementSystem.Areas.Users.Models;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Users.Repository;

public abstract class IUserRepository : Repository<User, string> {
	protected IUserRepository(DbContext context) : base(context) {
	}
}