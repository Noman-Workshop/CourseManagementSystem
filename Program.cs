using CourseManagementSystem.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbService(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthServices(builder.Configuration);

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
	"default",
	"{controller=Home}/{action=Index}/{id?}");

app.Run();
