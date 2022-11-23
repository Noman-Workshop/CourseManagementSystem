using CourseManagementSystem.Areas.Addresses.Repository;
using CourseManagementSystem.Areas.Budgets.Repositories;
using CourseManagementSystem.Areas.Budgets.Services;
using CourseManagementSystem.Areas.Budgets.UnitOfWorks;
using CourseManagementSystem.Areas.Departments.Repositories;
using CourseManagementSystem.Areas.Departments.Services;
using CourseManagementSystem.Areas.Students.Repositories;
using CourseManagementSystem.Areas.Students.Services;
using CourseManagementSystem.Areas.Students.UnitOfWorks;
using CourseManagementSystem.Areas.Teachers.Repositories;
using CourseManagementSystem.Areas.Teachers.Services;
using CourseManagementSystem.Areas.Teachers.UnitOfWorks;
using CourseManagementSystem.Areas.Users.Repository;
using CourseManagementSystem.Areas.Users.Services;

namespace CourseManagementSystem.Extensions;

public static class ApplicationServiceExtensions {
	public static void AddApplicationServices(this IServiceCollection services, IConfiguration config) {
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
