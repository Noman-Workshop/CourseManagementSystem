using System.Security.Claims;
using CourseManagementSystem.Areas.Login.Dto;

namespace CourseManagementSystem.Areas.Auth.Services;

public interface IAuthService {
	Task<bool> IsValid(LoginDto loginDto);
	ClaimsPrincipal SignIn(LoginDto loginDto);
}