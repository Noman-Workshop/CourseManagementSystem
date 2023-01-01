using System.Linq.Expressions;
using CourseManagementSystem.Areas.Users.Models;
using CourseManagementSystem.Areas.Users.Repository;

namespace CourseManagementSystem.Areas.Users.Services;

public class UserService : IUserService {
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository) {
		_userRepository = userRepository;
	}

	public async Task<List<User>> Find() => await _userRepository.Find();

	public async ValueTask<User> Find(string email) =>
		await _userRepository.Find(email) ?? throw new ArgumentException();

	public Task<List<User>> Find(Expression<Func<User, bool>> condition, string includeAttributes) =>
		_userRepository.Find(condition, includeAttributes);

	public Task Add(User entity) => throw new NotImplementedException();

	public Task Update(User entity) => throw new NotImplementedException();

	public Task Delete(User entity) => throw new NotImplementedException();

	public Task<bool> Exists(string email) => _userRepository.Find(email).AsTask().ContinueWith(u => u.Result != null);
}
