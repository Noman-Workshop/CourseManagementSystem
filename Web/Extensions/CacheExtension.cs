using Microsoft.EntityFrameworkCore;
using Models;

namespace CourseManagementSystem.Extensions;

public static class CacheExtension {
	public static void AddCacheService(this IServiceCollection services, IConfiguration configuration) {
		services.AddStackExchangeRedisCache(options => {
												options.Configuration =
													configuration.GetConnectionString("RedisConnection");
											});
	}
}
