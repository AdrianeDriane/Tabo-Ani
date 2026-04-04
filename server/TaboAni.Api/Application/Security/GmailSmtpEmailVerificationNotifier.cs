using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using TaboAni.Api.Application.Configuration;
using TaboAni.Api.Application.Interfaces.Security;

namespace TaboAni.Api.Application.Security;

public sealed class GmailSmtpEmailVerificationNotifier(
    IOptions<SmtpOptions> smtpOptions,
    ILogger<GmailSmtpEmailVerificationNotifier> logger) : IEmailVerificationNotifier
{
    private readonly SmtpOptions _smtpOptions = smtpOptions.Value;
    private readonly ILogger<GmailSmtpEmailVerificationNotifier> _logger = logger;

    public async Task NotifyAsync(
        string email,
        string verificationUrl,
        CancellationToken cancellationToken = default)
    {
        ValidateConfiguration();

        var message = BuildMessage(email, verificationUrl);

        using var client = new SmtpClient();

        try
        {
            await client.ConnectAsync(
                _smtpOptions.Host,
                _smtpOptions.Port,
                ResolveSocketOptions(_smtpOptions.UseStartTls),
                cancellationToken);

            await client.AuthenticateAsync(
                _smtpOptions.Username,
                _smtpOptions.Password,
                cancellationToken);

            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);

            _logger.LogInformation("Verification email dispatched to {Email}.", email);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Gmail SMTP delivery failed for {Email} via {Host}:{Port}.",
                email,
                _smtpOptions.Host,
                _smtpOptions.Port);

            throw new InvalidOperationException("Verification email delivery failed.");
        }
    }

    private void ValidateConfiguration()
    {
        if (string.IsNullOrWhiteSpace(_smtpOptions.Host) ||
            _smtpOptions.Port <= 0 ||
            string.IsNullOrWhiteSpace(_smtpOptions.Username) ||
            string.IsNullOrWhiteSpace(_smtpOptions.Password) ||
            string.IsNullOrWhiteSpace(_smtpOptions.FromEmail))
        {
            throw new InvalidOperationException("SMTP email delivery is not configured.");
        }
    }

    private MimeMessage BuildMessage(string email, string verificationUrl)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_smtpOptions.FromName, _smtpOptions.FromEmail));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Verify your Tabo-Ani account";

        var bodyBuilder = new BodyBuilder
        {
            TextBody = BuildPlainTextBody(verificationUrl),
            HtmlBody = BuildHtmlBody(verificationUrl)
        };

        message.Body = bodyBuilder.ToMessageBody();
        return message;
    }

    private static SecureSocketOptions ResolveSocketOptions(bool useStartTls)
    {
        return useStartTls
            ? SecureSocketOptions.StartTls
            : SecureSocketOptions.SslOnConnect;
    }

    private static string BuildPlainTextBody(string verificationUrl)
    {
        return $"""
            Welcome to Tabo-Ani.

            Verify your email address to activate your account and move your role applications into review:
            {verificationUrl}

            This link expires in 24 hours. If you did not create this account, you can ignore this message.
            """;
    }

    private static string BuildHtmlBody(string verificationUrl)
    {
        var encodedVerificationUrl = System.Net.WebUtility.HtmlEncode(verificationUrl);

        var builder = new StringBuilder();
        builder.AppendLine("<div style=\"font-family:Segoe UI,Arial,sans-serif;color:#1f2937;line-height:1.6;max-width:640px;margin:0 auto;padding:24px;\">");
        builder.AppendLine("<h1 style=\"font-size:24px;margin-bottom:16px;color:#111827;\">Verify your Tabo-Ani account</h1>");
        builder.AppendLine("<p style=\"margin-bottom:16px;\">Welcome to Tabo-Ani. Confirm your email address to activate your account and move your role applications into review.</p>");
        builder.AppendLine($"<p style=\"margin:24px 0;\"><a href=\"{encodedVerificationUrl}\" style=\"display:inline-block;background:#d97706;color:#ffffff;text-decoration:none;padding:12px 20px;border-radius:999px;font-weight:600;\">Verify Email</a></p>");
        builder.AppendLine($"<p style=\"margin-bottom:16px;\">If the button does not work, copy and paste this link into your browser:<br /><a href=\"{encodedVerificationUrl}\" style=\"color:#166534;word-break:break-all;\">{encodedVerificationUrl}</a></p>");
        builder.AppendLine("<p style=\"font-size:14px;color:#6b7280;\">This link expires in 24 hours. If you did not create this account, you can ignore this message.</p>");
        builder.AppendLine("</div>");
        return builder.ToString();
    }
}
