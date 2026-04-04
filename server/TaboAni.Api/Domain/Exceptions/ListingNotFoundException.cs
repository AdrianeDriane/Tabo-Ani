using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class ListingNotFoundException : DomainException
{
    public ListingNotFoundException(Guid listingId)
        : base("listing_not_found", $"Listing '{listingId}' was not found.", HttpStatusCode.NotFound)
    {
    }
}
