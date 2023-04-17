using System.Security.Claims;
using DTOs.Login;

namespace Services.Auth;

public interface IAuthService {
	object[] SignIn(LoginDto loginDto);
}