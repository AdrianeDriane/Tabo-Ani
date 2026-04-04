using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class InvalidListingPriceException : DomainException
{
    public InvalidListingPriceException(string message) : base(
        "invalid_listing_price",
        message,
        HttpStatusCode.BadRequest)
    {
    }
}
