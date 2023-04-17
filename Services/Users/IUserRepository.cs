using Microsoft.EntityFrameworkCore;
using Models;
using Services.Common;

namespace Services.Users;

public abstract class IUserRepository : Repository<User, Guid> {
	protected IUserRepository(DbContext context) : base(context) {
	}

}