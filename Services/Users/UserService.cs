using System.Linq.Expressions;
using Models;

namespace Services.Users;

class UserService : IUserService {
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository) {
		_userRepository = userRepository;
	}

	public Task<List<User>> Find() {
		return _userRepository.Find();
	}

	public ValueTask<User> Find(Guid id) {
		return _userRepository.Find(id);
	}

	public Task<List<User>> Find(Expression<Func<User, bool>> condition, string includeAttributes) {
		return _userRepository.Find(condition, includeAttributes);
	}

	public Task<List<User>> Find(Expression<Func<User, bool>> condition, Expression<Func<User, object>> includeAttributes) {
		return _userRepository.Find(condition, includeAttributes);
	}

	public Task Add(User entity) => throw new NotImplementedException();

	public Task Update(User entity) => throw new NotImplementedException();

	public Task Delete(User entity) => throw new NotImplementedException();

	public async Task<bool> Exists(Guid id) {
		return await _userRepository.Find(id) != null;
	}
}
