using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class ListingVehicleTypeNotAssignedException : DomainException
{
    public ListingVehicleTypeNotAssignedException(Guid listingId, Guid vehicleTypeId)
        : base(
            "listing_vehicle_type_not_assigned",
            $"Vehicle type '{vehicleTypeId}' is not assigned to listing '{listingId}'.",
            HttpStatusCode.NotFound)
    {
    }
}
