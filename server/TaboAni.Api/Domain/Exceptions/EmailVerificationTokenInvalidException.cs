using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class EmailVerificationTokenInvalidException(string message)
    : DomainException(
        "auth.email_verification_invalid",
        message,
        HttpStatusCode.BadRequest);
