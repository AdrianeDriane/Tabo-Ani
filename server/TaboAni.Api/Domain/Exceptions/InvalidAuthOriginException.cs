using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class InvalidAuthOriginException()
    : DomainException(
        "auth.invalid_origin",
        "The request origin is not allowed to use cookie-based authentication endpoints.",
        HttpStatusCode.Forbidden);
