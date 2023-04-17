using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Minio;
using Services.Addresses.Repository;
using Services.Calender;
using Services.Employees;
using Services.Leaves;
using Services.Leaves.LeaveCarriedRemaining;
using Services.Leaves.LeaveRequestReviews;
using Services.Leaves.LeaveRequests;
using Services.Leaves.LeaveTypes;
using Services.Mappers;
using Services.Storage;
using Services.Users;
using LeaveCarriedRemaining = Models.LeaveCarriedRemaining;

namespace Services.Extensions;

public static class ApplicationServiceExtensions {
	public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration) {
		services
			.AddScoped<MapperConfigurations, MapperConfigurations>()
			.AddScoped<IMapperService, MapperService>();

		services
			.AddSingleton<IMinioStorageService<MinioClient>, MinioStorageService>();

		services
			.AddScoped<IAddressRepository, AddressRepository>();

		services
			.AddScoped<IUserRepository, UserRepository>()
			.AddScoped<IUserService, UserService>();

		services
			.AddScoped<IEmployeeService, EmployeeService>()
			.AddScoped<IEmployeeRepository, EmployeeRepository>();

		services
			.AddScoped<ILeaveRequestService, LeaveRequestService>()
			.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>()
			.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>()
			.AddScoped<ILeaveCarriedRemaining, Leaves.LeaveCarriedRemaining.LeaveCarriedRemaining>()
			.AddScoped<ILeaveRequestReviewRepository, LeaveRequestReviewRepository>();

		services
			.AddScoped<ICalenderRepository, CalenderRepository>()
			.AddScoped<ICalendarService, CalendarService>();

		services.AddMvcCore(options => { options.EnableEndpointRouting = false; });
	}
}
