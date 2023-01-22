using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Addresses.Repository;
using Services.Users;
using Services.Users.Services;

namespace Services.Extensions;

public static class ApplicationServiceExtensions {
	public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration) {
		services
			.AddScoped<IUserRepository, UserRepository>()
			.AddScoped<IUserService, UserService>();

		services
			.AddScoped<IAddressRepository, AddressRepository>();
	}
}
