using Microsoft.Extensions.Configuration;
using CourseManagementSystem.Extensions;
using Services.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options => {
								options.IdleTimeout = TimeSpan.FromHours(3);
								options.Cookie.HttpOnly = true;
								options.Cookie.IsEssential = true;
							});

builder.Services.AddControllersWithViews();
builder.Services.AddDbService(builder.Configuration);
builder.Services.AddCacheService(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddAuthPolicies(builder.Configuration);

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
	"default",
	"{controller=Home}/{action=Index}/{id?}");

app.Run();
