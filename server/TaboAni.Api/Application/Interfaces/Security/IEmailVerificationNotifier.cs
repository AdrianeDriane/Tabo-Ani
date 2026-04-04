namespace TaboAni.Api.Application.Interfaces.Security;

public interface IEmailVerificationNotifier
{
    Task NotifyAsync(string email, string verificationUrl, CancellationToken cancellationToken = default);
}
