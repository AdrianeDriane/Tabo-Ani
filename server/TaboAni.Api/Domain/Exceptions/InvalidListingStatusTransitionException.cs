using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class InvalidListingStatusTransitionException : DomainException
{
    public InvalidListingStatusTransitionException(string message)
        : base("invalid_listing_status_transition", message, HttpStatusCode.BadRequest)
    {
    }
}
