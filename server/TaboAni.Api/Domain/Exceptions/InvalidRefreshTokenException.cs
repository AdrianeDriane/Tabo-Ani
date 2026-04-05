using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class InvalidRefreshTokenException()
    : DomainException(
        "auth.invalid_refresh_token",
        "Refresh token is missing, invalid, expired, or has been revoked.",
        HttpStatusCode.Unauthorized);
