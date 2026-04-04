using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class CartItemNotFoundException : DomainException
{
    public CartItemNotFoundException(Guid cartItemId) : base(
        "cart_item_not_found",
        $"Cart item '{cartItemId}' was not found.",
        HttpStatusCode.NotFound)
    {
    }
}
