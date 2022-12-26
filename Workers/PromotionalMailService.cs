using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace CourseManagementSystem.Workers;

public class PromotionalMailService : BackgroundService {
	protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
		// NEVER_EAT_POISON_Disable_CertificateValidation();
		while (!stoppingToken.IsCancellationRequested) {
			// send a mail with SMTP client
			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse("admin@rentigo.store"));
			email.To.Add(MailboxAddress.Parse("test2@rentigo.store"));
			email.Subject = "Promotional Mail";
			email.Body = new TextPart(TextFormat.Plain) { Text = "Promotional Mail" };

			using var smtp = new SmtpClient();
			smtp.CheckCertificateRevocation = false;
			await smtp.ConnectAsync("rentigo.store", 587, SecureSocketOptions.StartTls, stoppingToken);
			await smtp.AuthenticateAsync("admin@rentigo.store", "PASSWORD", stoppingToken);
			await smtp.SendAsync(email, stoppingToken);
			await smtp.DisconnectAsync(true, stoppingToken);

			Console.WriteLine("Promotional Mail Sent");
			// wait for 1 min
			await Task.Delay(60000, stoppingToken);
		}
	}
}
