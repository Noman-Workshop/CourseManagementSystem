using System.Security.Claims;
using DTOs.Login;
using Models;
using Models.Constants;
using Services.Users.Services;

namespace Services.Auth.Services;

public class AuthService : IAuthService {
	private readonly IUserService _userService;

	public AuthService(IUserService userService) {
		_userService = userService;
	}

	public async Task<User?> IsValid(string email, string password) {
		try {
			User user = (await _userService.Find(u => u.Email == email, "Roles"))[0];
			if (user.Password != password) {
				return null;
			}

			return user;
		} catch (Exception) {
			return null;
		}
	}

	public ClaimsPrincipal SignIn(LoginDto loginDto) {
		// User? user = IsValid(loginDto.UserEmail, loginDto.Password).Result;
		// if (user == null) {
		// 	throw new ArgumentException("Invalid credential");
		// }
		//
		// var claims = new List<Claim> {
		// 	new(ClaimTypes.Email, loginDto.UserEmail),
		// };
		// // string[] roles = user.UserRoles.Select(r => r.Role.Name).ToArray();
		// claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
		//
		// var identity = new ClaimsIdentity(claims, AuthTypes.UsernamePasswordCookies.ToString());
		// return new ClaimsPrincipal(identity);
		return null;
	}
}
