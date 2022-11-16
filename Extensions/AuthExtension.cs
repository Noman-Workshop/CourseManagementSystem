using CourseManagementSystem.Areas.Auth.Models;
using CourseManagementSystem.Areas.Auth.Services;

namespace CourseManagementSystem.Extensions;

public static class AuthExtension {
	public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration) {
		services.AddScoped<IAuthService, AuthService>();

		services.AddAuthentication(AuthTypes.UsernamePasswordCookies.ToString())
			.AddCookie(AuthTypes.UsernamePasswordCookies.ToString(),
				options => {
					options.LoginPath = "/Login";
					options.Cookie.Name = AuthTypes.UsernamePasswordCookies.ToString();
				});
	}
}
