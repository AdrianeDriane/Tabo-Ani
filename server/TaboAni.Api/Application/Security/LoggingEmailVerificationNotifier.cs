using TaboAni.Api.Application.Interfaces.Security;

namespace TaboAni.Api.Application.Security;

public sealed class LoggingEmailVerificationNotifier(ILogger<LoggingEmailVerificationNotifier> logger)
    : IEmailVerificationNotifier
{
    private readonly ILogger<LoggingEmailVerificationNotifier> _logger = logger;

    public Task NotifyAsync(string email, string token, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Email verification token generated for {Email}. Token: {Token}",
            email,
            token);

        return Task.CompletedTask;
    }
}
