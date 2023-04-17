using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DTOs.Login;
using Models;
using Services.Users;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Models.Constants;

namespace Services.Auth;

public class AuthService : IAuthService {
	private readonly IUserService _userService;

	public AuthService(IUserService userService) {
		_userService = userService;
	}

	private static string HashSHA512(string password) {
		using (SHA512 sha512 = SHA512.Create()) {
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			byte[] hash = sha512.ComputeHash(bytes);

			return BitConverter.ToString(hash).Replace("-", "").ToLower();
		}
	}

	private async Task<User?> IsValid(string email, string password) {
		try {
			User user = (await _userService.Find(u => u.Email == email, u => u.UserRoles))[0];
			var hashedPassword = HashSHA512(password);
			return user.Password == hashedPassword ? user : null;
		} catch (Exception) {
			return null;
		}
	}

	public object[] SignIn(LoginDto loginDto) {
		User? user = IsValid(loginDto.UserEmail, loginDto.Password).Result;
		if (user == null) {
			throw new ArgumentException("Invalid credential");
		}

		var claims = new List<Claim> {
			new(ClaimTypes.Email, loginDto.UserEmail),
			new(ClaimTypes.Name, user.LastName),
			new(ClaimTypes.Email, user.Email),
		};
		// // string[] roles = user.UserRoles.Select(r => r.Role.Name).ToArray();
		// claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
		//
		var identity = new ClaimsIdentity(claims, AuthTypes.UsernamePasswordCookies.ToString());
		var principal = new ClaimsPrincipal(identity);
		// return an array of user and the principal
		return new object[] { user, principal };
	}
}
