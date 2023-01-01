using CourseManagementSystem.Areas.Budgets.Repositories;
using CourseManagementSystem.Areas.Budgets.UnitOfWorks;
using CourseManagementSystem.Areas.Departments.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Addresses.Repository;
using Services.Budgets.Services;
using Services.Departments.Repositories;
using Services.Students.Repositories;
using Services.Students.Services;
using Services.Students.UnitOfWorks;
using Services.Teachers.Repositories;
using Services.Teachers.Services;
using Services.Teachers.UnitOfWorks;
using Services.Users.Repository;
using Services.Users.Services;

namespace Services.Extensions;

public static class ApplicationServiceExtensions {
	public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration) {
		services
			.AddScoped<IUserRepository, UserRepository>()
			.AddScoped<IUserService, UserService>();

		services
			.AddScoped<IAddressRepository, AddressRepository>();

		services
			.AddScoped<ITeacherRepository, TeacherRepository>()
			.AddScoped<ITeacherUnitOfWork, TeacherUnitOfWork>()
			.AddScoped<ITeacherService, TeacherService>();

		services
			.AddScoped<IStudentRepository, StudentRepository>()
			.AddScoped<IStudentUnitOfWork, StudentUnitOfWork>()
			.AddScoped<IStudentService, StudentService>();

		services
			.AddScoped<IDepartmentRepository, DepartmentRepository>()
			.AddScoped<IDepartmentService, DepartmentService>();

		services
			.AddScoped<IBudgetRepository, BudgetRepository>()
			.AddScoped<IBudgetUnitOfWork, BudgetUnitOfWork>()
			.AddScoped<IBudgetService, BudgetService>();

		services
			.AddScoped<IBudgetAuditLogRepository, BudgetAuditLogRepository>();
	}
}
