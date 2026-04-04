using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class InvalidListingQueryException : DomainException
{
    public InvalidListingQueryException(string message) : base("invalid_listing_query", message, HttpStatusCode.BadRequest)
    {
    }
}
