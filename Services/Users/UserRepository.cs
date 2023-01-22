using Microsoft.EntityFrameworkCore;

namespace Services.Users;

class UserRepository : IUserRepository {
	public UserRepository(DbContext context) : base(context) {
	}
}
