using System.Security.Claims;
using Models;

namespace Services.Auth.Services;

public interface IAuthService {
	Task<User?> IsValid(string email, string password);
	ClaimsPrincipal SignIn(string email, string password);
}