namespace TaboAni.Api.Application.DTOs.Response;

public sealed class SignupResponseDto
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = string.Empty;
    public string? MobileNumber { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? DisplayName { get; init; }
    public string RoleCode { get; init; } = string.Empty;
    public string AccountStatus { get; init; } = string.Empty;
}
