using System.Security.Claims;
using CourseManagementSystem.Areas.Auth.Models;
using CourseManagementSystem.Areas.Login.Dto;
using CourseManagementSystem.Areas.Users.Models;
using CourseManagementSystem.Areas.Users.Services;

namespace CourseManagementSystem.Areas.Auth.Services;

class AuthService : IAuthService {
	private IUserService _userService;

	public AuthService(IUserService userService) {
		_userService = userService;
	}

	public async Task<User?> IsValid(LoginDto loginDto) {
		try {
			User user = (await _userService.Find(u => u.Email == loginDto.UserEmail, "Roles"))[0];
			if (user.Password != loginDto.Password) {
				return null;
			}

			return user;
		} catch (Exception) {
			return null;
		}
	}

	public ClaimsPrincipal SignIn(LoginDto loginDto) {
		User? user = IsValid(loginDto).Result;
		if (user == null) {
			throw new ArgumentException("Invalid credential");
		}

		var claims = new List<Claim> {
			new(ClaimTypes.Email, loginDto.UserEmail),
		};
		string[] roles = user.Roles.Select(r => r.Name).ToArray();
		claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

		var identity = new ClaimsIdentity(claims, AuthTypes.UsernamePasswordCookies.ToString());
		return new ClaimsPrincipal(identity);
	}
}
