using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class InvalidSignupRequestException(string message)
    : DomainException(
        "signup.invalid_request",
        message,
        HttpStatusCode.BadRequest);
