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

	public async Task<bool> IsValid(LoginDto loginDto) {
		try {
			User user = await _userService.Find(loginDto.UserEmail);
			if (user.Password != loginDto.Password) {
				return false;
			}

			return true;
		} catch (Exception) {
			return false;
		}
	}

	public ClaimsPrincipal SignIn(LoginDto loginDto) {
		if (!IsValid(loginDto).Result) {
			throw new ArgumentException("Invalid credential");
		}

		var claims = new List<Claim> {
			new(ClaimTypes.Email, loginDto.UserEmail),
		};

		var identity = new ClaimsIdentity(claims, AuthTypes.UsernamePasswordCookies.ToString());
		return new ClaimsPrincipal(identity);
	}
}
