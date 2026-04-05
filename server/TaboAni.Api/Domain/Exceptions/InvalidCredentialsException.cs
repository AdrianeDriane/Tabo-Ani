using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class InvalidCredentialsException()
    : DomainException(
        "auth.invalid_credentials",
        "Email or password is incorrect.",
        HttpStatusCode.Unauthorized);
