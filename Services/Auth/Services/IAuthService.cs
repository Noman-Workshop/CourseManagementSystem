using System.Security.Claims;
using DTOs.Login;
using Models;

namespace Services.Auth.Services;

public interface IAuthService {
	ClaimsPrincipal SignIn(LoginDto loginDto);
}