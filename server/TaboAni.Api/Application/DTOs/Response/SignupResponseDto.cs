namespace TaboAni.Api.Application.DTOs.Response;

public sealed class SignupResponseDto
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = string.Empty;
    public string? MobileNumber { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? DisplayName { get; init; }
    public bool IsEmailVerified { get; init; }
    public string AccountStatus { get; init; } = string.Empty;
    public IReadOnlyList<SignupRoleApplicationResponseDto> RequestedRoles { get; init; } =
        Array.Empty<SignupRoleApplicationResponseDto>();
}

public sealed class SignupRoleApplicationResponseDto
{
    public string RoleCode { get; init; } = string.Empty;
    public Guid KycApplicationId { get; init; }
    public string ApplicationStatus { get; init; } = string.Empty;
}

public sealed class EmailVerificationStatusResponseDto
{
    public string Status { get; init; } = string.Empty;
    public Guid UserId { get; init; }
    public string Email { get; init; } = string.Empty;
    public bool IsEmailVerified { get; init; }
    public DateTimeOffset? VerifiedAt { get; init; }
}

public sealed class ResendEmailVerificationResponseDto
{
    public string Status { get; init; } = string.Empty;
}
