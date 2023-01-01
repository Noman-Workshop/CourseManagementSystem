using PromotionalMailWorker;
using PromotionalMailWorker.Extensions;
using Services.Extensions;

IHostBuilder builder = Host.CreateDefaultBuilder(args);
IHost host = builder.ConfigureServices((context, services) => {
											services.AddHostedService<Worker>();
											services.AddDbService(context.Configuration);
											services.AddApplicationServices(context.Configuration);
										})
	.Build();

await host.RunAsync();
