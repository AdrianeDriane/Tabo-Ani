using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class CartIntegrityException : DomainException
{
    public CartIntegrityException(string message) : base("cart_integrity_error", message, HttpStatusCode.Conflict)
    {
    }
}
