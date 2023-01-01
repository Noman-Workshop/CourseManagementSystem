using Microsoft.EntityFrameworkCore;

namespace Services.Users.Repository;

public class UserRepository : IUserRepository {
	public UserRepository(DbContext context) : base(context) {
	}
}
