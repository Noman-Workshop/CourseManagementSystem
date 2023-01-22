using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services.Users;

public abstract class IUserRepository : Repository<User, Guid> {
	protected IUserRepository(DbContext context) : base(context) {
	}
}