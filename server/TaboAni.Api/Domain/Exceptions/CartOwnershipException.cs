using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class CartOwnershipException : DomainException
{
    public CartOwnershipException(Guid cartItemId, Guid userId) : base(
        "cart_ownership_error",
        $"Cart item '{cartItemId}' does not belong to user '{userId}'.",
        HttpStatusCode.Forbidden)
    {
    }
}
