namespace TaboAni.Api.Domain.Entities;

public class EmailVerificationToken
{
    public Guid EmailVerificationTokenId { get; set; }
    public Guid UserId { get; set; }
    public string TokenHash { get; set; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? ConsumedAt { get; set; }
    public DateTimeOffset? InvalidatedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
