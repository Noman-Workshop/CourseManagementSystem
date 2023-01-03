using System.Security.Claims;
using Models;
using Models.Constants;
using Services.Auth.Services;

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
			options.AddPolicy(Policies.ADMIN, policy => policy.RequireClaim(ClaimTypes.Role, Roles.ADMIN.ToString()));
			options.AddPolicy(Policies.TEACHER, policy => policy.RequireClaim(ClaimTypes.Role, Roles.TEACHER.ToString()));
			options.AddPolicy(Policies.STUDENT, policy => policy.RequireClaim(ClaimTypes.Role, Roles.STUDENT.ToString()));
		});
	}
}
