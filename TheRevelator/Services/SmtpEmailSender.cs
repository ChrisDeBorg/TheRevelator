using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace TheRevelator.Services;

public class SmtpEmailSender : IEmailSender
{
    private readonly MailSettings _settings;
    public SmtpEmailSender(IOptions<MailSettings> options) => _settings = options.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        using var mail = new MailMessage();
        mail.From = new MailAddress(_settings.From);
        mail.To.Add(email);
        mail.Subject = subject;
        mail.Body = htmlMessage;
        mail.IsBodyHtml = true;

        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = _settings.UseSSL,
            Credentials = new NetworkCredential(_settings.User, _settings.Password)
        };

        await client.SendMailAsync(mail);
    }
}
