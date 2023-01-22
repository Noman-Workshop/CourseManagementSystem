using System.Linq.Expressions;
using Models;
using Services.Users.Services;

namespace Services.Users;

class UserService : IUserService {
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository) {
		_userRepository = userRepository;
	}

	public Task<List<User>> Find() {
		return _userRepository.Find();
	}

	public ValueTask<User> Find(Guid id) => throw new NotImplementedException();

	public Task<List<User>> Find(Expression<Func<User, bool>> condition, string includeAttributes) =>
		throw new NotImplementedException();

	public Task Add(User entity) => throw new NotImplementedException();

	public Task Update(User entity) => throw new NotImplementedException();

	public Task Delete(User entity) => throw new NotImplementedException();

	public Task<bool> Exists(Guid id) => throw new NotImplementedException();
}
