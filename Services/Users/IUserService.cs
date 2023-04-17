using Models;
using Services.Common;

namespace Services.Users;

public interface IUserService : IService<User, Guid> {
}