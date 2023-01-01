using CourseManagementSystem.Areas.Teachers.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace CourseManagementSystem.Workers;

public class PromotionalMailService : BackgroundService {
	private readonly ILogger<PromotionalMailService> _logger;
	private readonly ITeacherService _teacherService;

	public PromotionalMailService(
		ILogger<PromotionalMailService> logger,
		IServiceScopeFactory scopeFactory
	) {
		_logger = logger;
		_teacherService = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ITeacherService>();
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
		while (!stoppingToken.IsCancellationRequested) {
			// send a mail with SMTP client
			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse("admin@rentigo.store"));
			var teachers = await _teacherService.Find();
			foreach (var teacher in teachers) {
				email.To.Add(MailboxAddress.Parse(teacher.Email));
			}
			email.Subject = "Promotional Mail";
			email.Body = new TextPart(TextFormat.Plain) { Text = "Promotional Mail" };

			using var smtp = new SmtpClient();
			smtp.CheckCertificateRevocation = false;
			await smtp.ConnectAsync("rentigo.store", 587, SecureSocketOptions.StartTls, stoppingToken);
			await smtp.AuthenticateAsync("admin@rentigo.store", "PASSWORD", stoppingToken);
			await smtp.SendAsync(email, stoppingToken);
			await smtp.DisconnectAsync(true, stoppingToken);

			_logger.LogInformation("Promotional Mail sent at: {Time}", DateTimeOffset.Now);
			// wait for 1 min
			await Task.Delay(60000, stoppingToken);
		}
	}
}
