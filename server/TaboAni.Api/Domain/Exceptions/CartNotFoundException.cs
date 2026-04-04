using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class CartNotFoundException : DomainException
{
    public CartNotFoundException(Guid userId) : base(
        "cart_not_found",
        $"Active cart for user '{userId}' was not found.",
        HttpStatusCode.NotFound)
    {
    }
}
