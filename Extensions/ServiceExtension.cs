using CourseManagementSystem.Areas.Addresses.Repository;
using CourseManagementSystem.Areas.Teachers.Repositories;
using CourseManagementSystem.Areas.Teachers.Services;
using CourseManagementSystem.Areas.Teachers.UnitOfWorks;

namespace CourseManagementSystem.Extensions;

public static class ApplicationServiceExtensions {
	public static void AddApplicationServices(this IServiceCollection services, IConfiguration config) {

		services
			.AddScoped<IAddressRepository, AddressRepository>();

		services
			.AddScoped<ITeacherRepository, TeacherRepository>()
			.AddScoped<ITeacherUnitOfWork, TeacherUnitOfWork>()
			.AddScoped<ITeacherService, TeacherService>();
	}
}
