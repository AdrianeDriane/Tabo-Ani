using System.Net.Mail;
using TaboAni.Api.Application.Configuration;
using TaboAni.Api.Application.DTOs.Request;

namespace TaboAni.Api.Application.Validation.Auth;

internal static class AuthValidationHelper
{
    private const string BuyerRoleCode = "BUYER";
    private const string FarmerRoleCode = "FARMER";

    public static ValidatedSignupRequest ValidateSignupRequest(
        SignupRequestDto signupRequestDto,
        SignupPolicyOptions signupPolicyOptions)
    {
        ArgumentNullException.ThrowIfNull(signupRequestDto);
        ArgumentNullException.ThrowIfNull(signupPolicyOptions);

        var email = NormalizeEmail(signupRequestDto.Email);
        var mobileNumber = NormalizeMobileNumber(signupRequestDto.MobileNumber);
        var password = RequireValue(signupRequestDto.Password, "Password is required.");
        var confirmPassword = RequireValue(signupRequestDto.ConfirmPassword, "Confirm password is required.");
        var firstName = RequireValue(signupRequestDto.FirstName, "First name is required.");
        var lastName = RequireValue(signupRequestDto.LastName, "Last name is required.");
        var displayName = NormalizeOptional(signupRequestDto.DisplayName);
        var termsVersion = RequireValue(signupRequestDto.TermsVersion, "Terms version is required.");
        var privacyVersion = RequireValue(signupRequestDto.PrivacyVersion, "Privacy version is required.");

        if (password.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters long.", nameof(signupRequestDto));
        }

        if (!string.Equals(password, confirmPassword, StringComparison.Ordinal))
        {
            throw new ArgumentException("Password and confirm password must match.", nameof(signupRequestDto));
        }

        if (!signupRequestDto.HasAcceptedTerms)
        {
            throw new ArgumentException("Terms of Service must be accepted.", nameof(signupRequestDto));
        }

        if (!signupRequestDto.HasAcceptedPrivacy)
        {
            throw new ArgumentException("Privacy Policy must be accepted.", nameof(signupRequestDto));
        }

        if (!string.Equals(termsVersion, signupPolicyOptions.TermsVersion, StringComparison.Ordinal))
        {
            throw new ArgumentException("Terms version is invalid or outdated.", nameof(signupRequestDto));
        }

        if (!string.Equals(privacyVersion, signupPolicyOptions.PrivacyVersion, StringComparison.Ordinal))
        {
            throw new ArgumentException("Privacy version is invalid or outdated.", nameof(signupRequestDto));
        }

        var applications = new List<ValidatedRoleApplication>();

        if (signupRequestDto.BuyerApplication is not null)
        {
            applications.Add(new ValidatedRoleApplication(
                BuyerRoleCode,
                RequireValue(signupRequestDto.BuyerApplication.BusinessName, "Buyer business name is required."),
                RequireValue(signupRequestDto.BuyerApplication.LocationText, "Buyer location is required."),
                RequireValue(signupRequestDto.BuyerApplication.BusinessType, "Buyer business type is required.")));
        }

        if (signupRequestDto.FarmerApplication is not null)
        {
            applications.Add(new ValidatedRoleApplication(
                FarmerRoleCode,
                RequireValue(signupRequestDto.FarmerApplication.FarmName, "Farm name is required."),
                RequireValue(signupRequestDto.FarmerApplication.LocationText, "Farm location is required."),
                null));
        }

        if (applications.Count == 0)
        {
            throw new ArgumentException("At least one role application is required.", nameof(signupRequestDto));
        }

        return new ValidatedSignupRequest(
            email,
            mobileNumber,
            password,
            firstName,
            lastName,
            displayName,
            termsVersion,
            privacyVersion,
            applications);
    }

    public static string ValidateEmailAddress(string email)
    {
        return NormalizeEmail(email);
    }

    public static string ValidateVerificationToken(string token)
    {
        return RequireValue(token, "Verification token is required.");
    }

    public static ValidatedLoginRequest ValidateLoginRequest(LoginRequestDto loginRequestDto)
    {
        ArgumentNullException.ThrowIfNull(loginRequestDto);

        return new ValidatedLoginRequest(
            NormalizeEmail(loginRequestDto.Email),
            RequireValue(loginRequestDto.Password, "Password is required."),
            loginRequestDto.RememberMe);
    }

    private static string NormalizeEmail(string email)
    {
        var normalizedEmail = RequireValue(email, "Email is required.").ToLowerInvariant();

        try
        {
            _ = new MailAddress(normalizedEmail);
        }
        catch (FormatException)
        {
            throw new ArgumentException("Email address is invalid.", nameof(email));
        }

        return normalizedEmail;
    }

    private static string? NormalizeMobileNumber(string? mobileNumber)
    {
        var normalizedMobileNumber = NormalizeOptional(mobileNumber);

        if (normalizedMobileNumber is null)
        {
            return null;
        }

        normalizedMobileNumber = normalizedMobileNumber.Replace(" ", string.Empty, StringComparison.Ordinal)
            .Replace("-", string.Empty, StringComparison.Ordinal)
            .Replace("(", string.Empty, StringComparison.Ordinal)
            .Replace(")", string.Empty, StringComparison.Ordinal);

        var digitCount = normalizedMobileNumber.Count(char.IsDigit);

        if (digitCount < 10 || digitCount > 15)
        {
            throw new ArgumentException("Mobile number must contain between 10 and 15 digits.", nameof(mobileNumber));
        }

        return normalizedMobileNumber;
    }

    private static string RequireValue(string? value, string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(errorMessage);
        }

        return value.Trim();
    }

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}

internal sealed record ValidatedSignupRequest(
    string Email,
    string? MobileNumber,
    string Password,
    string FirstName,
    string LastName,
    string? DisplayName,
    string TermsVersion,
    string PrivacyVersion,
    IReadOnlyList<ValidatedRoleApplication> RoleApplications);

internal sealed record ValidatedRoleApplication(
    string RoleCode,
    string Name,
    string LocationText,
    string? BusinessType);

internal sealed record ValidatedLoginRequest(
    string Email,
    string Password,
    bool RememberMe);
