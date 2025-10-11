using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using ServiceAbstraction;

namespace Service;

public class EmailService(IConfiguration configuration):IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(configuration["EmailSettings:From"]));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) {Text = body};
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(configuration["EmailSettings:Host"], int.Parse(configuration["EmailSettings:Port"]!), true);
        await smtp.AuthenticateAsync(configuration["EmailSettings:Username"], configuration["EmailSettings:Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}