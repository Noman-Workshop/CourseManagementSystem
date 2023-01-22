using System.Security.Claims;
using Models;
using Models.Constants;
using Services.Auth.Services;
using Role = Models.Constants.Role;

namespace CourseManagementSystem.Extensions;

public static class AuthExtension {
	public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration) {
		services.AddScoped<IAuthService, AuthService>();

		services.AddAuthentication(AuthTypes.UsernamePasswordCookies.ToString())
			.AddCookie(AuthTypes.UsernamePasswordCookies.ToString(),
				options => {
					options.LoginPath = "/Auth";
					options.Cookie.Name = AuthTypes.UsernamePasswordCookies.ToString();
				});
	}

	public static void AddAuthPolicies(this IServiceCollection services, IConfiguration configuration) {
		services.AddAuthorization(options => {
			options.AddPolicy(Policy.ADMIN, policy => policy.RequireClaim(ClaimTypes.Role, Role.ADMIN.ToString()));
			options.AddPolicy(Policy.TEACHER, policy => policy.RequireClaim(ClaimTypes.Role, Role.TEACHER.ToString()));
			options.AddPolicy(Policy.STUDENT, policy => policy.RequireClaim(ClaimTypes.Role, Role.STUDENT.ToString()));
		});
	}
}
