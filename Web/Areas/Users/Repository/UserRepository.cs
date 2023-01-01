using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Areas.Users.Repository;

class UserRepository : IUserRepository {
	public UserRepository(DbContext context) : base(context) {
	}
}
