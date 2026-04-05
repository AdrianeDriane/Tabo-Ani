namespace TaboAni.Api.Application.DTOs.Response;

public sealed class SessionResponseDto
{
    public string AccessToken { get; init; } = string.Empty;
    public DateTimeOffset AccessTokenExpiresAt { get; init; }
    public SessionUserResponseDto User { get; init; } = new();
}

public sealed class SessionUserResponseDto
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = string.Empty;
    public string? MobileNumber { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? DisplayName { get; init; }
    public bool IsEmailVerified { get; init; }
    public string AccountStatus { get; init; } = string.Empty;
    public DateTimeOffset? LastLoginAt { get; init; }
    public IReadOnlyList<string> Roles { get; init; } = Array.Empty<string>();
}
