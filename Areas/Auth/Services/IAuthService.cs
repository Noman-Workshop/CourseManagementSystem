using System.Security.Claims;
using CourseManagementSystem.Areas.Login.Dto;
using CourseManagementSystem.Areas.Users.Models;

namespace CourseManagementSystem.Areas.Auth.Services;

public interface IAuthService {
	Task<User?> IsValid(LoginDto loginDto);
	ClaimsPrincipal SignIn(LoginDto loginDto);
}