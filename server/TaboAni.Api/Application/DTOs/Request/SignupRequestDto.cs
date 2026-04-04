namespace TaboAni.Api.Application.DTOs.Request;

public sealed class SignupRequestDto
{
    public string Email { get; init; } = string.Empty;
    public string? MobileNumber { get; init; }
    public string Password { get; init; } = string.Empty;
    public string ConfirmPassword { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? DisplayName { get; init; }
    public string RoleCode { get; init; } = string.Empty;
    public string BusinessName { get; init; } = string.Empty;
    public string LocationText { get; init; } = string.Empty;
}
