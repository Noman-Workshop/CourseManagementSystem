using System.Security.Claims;
using CourseManagementSystem.Areas.Auth.Models;
using CourseManagementSystem.Areas.Login.Dto;

namespace CourseManagementSystem.Areas.Auth.Services;

class AuthService : IAuthService {
	public bool IsValid(LoginDto loginDto) {
		return loginDto.Username == "admin" && loginDto.Password == "password";
	}

	public ClaimsPrincipal SignIn(LoginDto loginDto) {
		if (!IsValid(loginDto)) {
			throw new ArgumentException("Invalid credential");
		}

		var claims = new List<Claim> {
			new(ClaimTypes.Name, loginDto.Username),
			new(ClaimTypes.Role, "Administrator"),
			new(ClaimTypes.Role, "Teacher"),
		};

		var identity = new ClaimsIdentity(claims, AuthTypes.UsernamePasswordCookies.ToString());
		return new ClaimsPrincipal(identity);
	}
}
