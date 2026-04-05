namespace TaboAni.Api.Domain.Entities;

public class RefreshToken
{
    public Guid RefreshTokenId { get; set; }
    public Guid UserId { get; set; }
    public string TokenHash { get; set; } = string.Empty;
    public bool IsPersistent { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? InvalidatedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
