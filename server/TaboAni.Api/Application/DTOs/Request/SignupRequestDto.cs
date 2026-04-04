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
    public bool HasAcceptedTerms { get; init; }
    public string TermsVersion { get; init; } = string.Empty;
    public bool HasAcceptedPrivacy { get; init; }
    public string PrivacyVersion { get; init; } = string.Empty;
    public SignupBuyerApplicationRequestDto? BuyerApplication { get; init; }
    public SignupFarmerApplicationRequestDto? FarmerApplication { get; init; }
}

public sealed class SignupBuyerApplicationRequestDto
{
    public string BusinessName { get; init; } = string.Empty;
    public string BusinessType { get; init; } = string.Empty;
    public string LocationText { get; init; } = string.Empty;
}

public sealed class SignupFarmerApplicationRequestDto
{
    public string FarmName { get; init; } = string.Empty;
    public string LocationText { get; init; } = string.Empty;
}

public sealed class ResendEmailVerificationRequestDto
{
    public string Email { get; init; } = string.Empty;
}

public sealed class VerifyEmailRequestDto
{
    public string Token { get; init; } = string.Empty;
}
