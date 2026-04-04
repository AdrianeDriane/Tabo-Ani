using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class InvalidCartException : DomainException
{
    public InvalidCartException(string message) : base("invalid_cart", message, HttpStatusCode.BadRequest)
    {
    }
}
