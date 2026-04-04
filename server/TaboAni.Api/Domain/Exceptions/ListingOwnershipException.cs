using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class ListingOwnershipException : DomainException
{
    public ListingOwnershipException(Guid listingId, Guid farmerProfileId)
        : base(
            "listing_ownership_violation",
            $"Listing '{listingId}' does not belong to farmer profile '{farmerProfileId}'.",
            HttpStatusCode.Forbidden)
    {
    }
}
