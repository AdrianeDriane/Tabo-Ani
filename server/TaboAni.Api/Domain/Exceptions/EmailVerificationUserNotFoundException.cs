using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class EmailVerificationUserNotFoundException(string email)
    : DomainException(
        "auth.email_not_found",
        $"No account was found for {email}.",
        HttpStatusCode.NotFound);
