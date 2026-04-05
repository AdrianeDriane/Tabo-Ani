namespace TaboAni.Api.Application.Configuration;

public sealed class AuthOptions
{
    public const string SectionName = "Auth";

    public string Issuer { get; init; } = "TaboAni.Api";
    public string Audience { get; init; } = "TaboAni.Client";
    public string SigningKey { get; init; } = string.Empty;
    public int AccessTokenLifetimeMinutes { get; init; } = 15;
    public int RefreshTokenLifetimeDays { get; init; } = 7;
    public string RefreshTokenCookieName { get; init; } = "taboani_refresh_token";
    public string RefreshTokenCookiePath { get; init; } = "/api/v1/auth";
    public string RefreshTokenCookieSameSite { get; init; } = "None";
    public bool UseSecureRefreshTokenCookie { get; init; } = true;
}

public sealed class FrontendOptions
{
    public const string SectionName = "Frontend";

    public string ClientAppBaseUrl { get; init; } = "http://localhost:5173";
    public string[] AllowedOrigins { get; init; } = ["http://localhost:5173"];
}

public sealed class EmailVerificationOptions
{
    public const string SectionName = "EmailVerification";

    public string VerificationPath { get; init; } = "/verify-email";
    public int TokenLifetimeHours { get; init; } = 24;
    public int ResendCooldownSeconds { get; init; } = 60;
}

public sealed class SignupPolicyOptions
{
    public const string SectionName = "SignupPolicies";

    public string TermsVersion { get; init; } = "2026-04-05";
    public string PrivacyVersion { get; init; } = "2026-04-05";
}

public sealed class SmtpOptions
{
    public const string SectionName = "Smtp";

    public string Host { get; init; } = "smtp.gmail.com";
    public int Port { get; init; } = 587;
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string FromEmail { get; init; } = string.Empty;
    public string FromName { get; init; } = "Tabo-Ani";
    public bool UseStartTls { get; init; } = true;
}

public sealed class AuthRateLimitOptions
{
    public const string SectionName = "RateLimiting:Auth";

    public int LoginPermitLimit { get; init; } = 5;
    public int LoginWindowMinutes { get; init; } = 15;
    public int RefreshPermitLimit { get; init; } = 30;
    public int RefreshWindowMinutes { get; init; } = 5;
    public int SignupPermitLimit { get; init; } = 5;
    public int SignupWindowMinutes { get; init; } = 15;
    public int ResendPermitLimit { get; init; } = 3;
    public int ResendWindowMinutes { get; init; } = 15;
    public int VerifyPermitLimit { get; init; } = 10;
    public int VerifyWindowMinutes { get; init; } = 15;
    public int QueueLimit { get; init; } = 0;
}
