using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementSystem.Extensions;

public static class DbExtension {
	public static void AddDbService(this IServiceCollection services, IConfiguration configuration) {
		services.AddScoped<DbContext, CMSDbContext>();
		services.AddDbContext<CMSDbContext>(
			options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
	}
}
