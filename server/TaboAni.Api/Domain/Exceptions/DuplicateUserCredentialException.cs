using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class DuplicateUserCredentialException(string credentialName)
    : DomainException(
        "signup.duplicate_credential",
        $"{credentialName} is already in use.",
        HttpStatusCode.Conflict);
