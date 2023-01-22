using Microsoft.EntityFrameworkCore;
using Models;

namespace PromotionalMailWorker.Extensions;

public static class DbExtension {
	public static void AddDbService(this IServiceCollection services, IConfiguration configuration) {
		services.AddScoped<DbContext, CMSDbContext>();
		services.AddDbContext<CMSDbContext>(
			options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
	}
}
