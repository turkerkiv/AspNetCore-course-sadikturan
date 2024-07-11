
using System.Net;
using System.Net.Mail;

namespace IdentityApp.Models;

public class SmtpEmailSender : IEmailSender
{
    readonly string? _host;
    readonly int _port;
    readonly bool _enabledSSL;
    readonly string? _username;
    readonly string? _password;

    public SmtpEmailSender(string? host, int port, bool enableSSL, string? username, string? password)
    {
        _host = host;
        _port = port;
        _enabledSSL = enableSSL;
        _username = username;
        _password = password;
    }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient(_host, _port)
        {
            Credentials = new NetworkCredential(_username, _password),
            EnableSsl = _enabledSSL,
        };

        return client.SendMailAsync(new MailMessage(_username ?? "", email, subject, message) { IsBodyHtml = true });
    }
}