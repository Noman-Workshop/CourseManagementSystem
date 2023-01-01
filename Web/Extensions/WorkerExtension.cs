using CourseManagementSystem.Workers;

namespace CourseManagementSystem.Extensions;

public static class WorkerExtension {
	public static void AddWorkerServices(this IServiceCollection services, IConfiguration configuration) {
		services.AddHostedService<PromotionalMailService>();
	}
}
