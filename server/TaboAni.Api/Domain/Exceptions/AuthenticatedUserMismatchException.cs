using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class AuthenticatedUserMismatchException(Guid authenticatedUserId, Guid requestedUserId)
    : DomainException(
        "auth.user_context_mismatch",
        $"Authenticated user '{authenticatedUserId}' cannot access user context '{requestedUserId}'.",
        HttpStatusCode.Forbidden);
