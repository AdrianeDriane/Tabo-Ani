using TaboAni.Api.Application.Interfaces.Security;

namespace TaboAni.Api.Application.Security;

public sealed class LoggingEmailVerificationNotifier(ILogger<LoggingEmailVerificationNotifier> logger)
    : IEmailVerificationNotifier
{
    private readonly ILogger<LoggingEmailVerificationNotifier> _logger = logger;

    public Task NotifyAsync(string email, string verificationUrl, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Email verification requested for {Email}. A delivery implementation should send the verification link without logging secrets.",
            email);

        return Task.CompletedTask;
    }
}
