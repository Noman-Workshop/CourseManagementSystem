using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Users.Repository;

public abstract class IUserRepository : Repository<User, string> {
	protected IUserRepository(DbContext context) : base(context) {
	}
}