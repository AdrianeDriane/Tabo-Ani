using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class UserNotFoundException : DomainException
{
    public UserNotFoundException(Guid userId) : base(
        "user_not_found",
        $"User '{userId}' was not found.",
        HttpStatusCode.NotFound)
    {
    }
}
