namespace TaboAni.Api.Application.DTOs.Response;

public static class CurrentSessionStatuses
{
    public const string Authenticated = "AUTHENTICATED";
    public const string Anonymous = "ANONYMOUS";
    public const string SessionExpired = "SESSION_EXPIRED";
    public const string AccountBlocked = "ACCOUNT_BLOCKED";
}

public sealed class CurrentSessionResponseDto
{
    public string Status { get; init; } = CurrentSessionStatuses.Anonymous;
    public SessionResponseDto? Session { get; init; }
    public string? AccountStatus { get; init; }
}
