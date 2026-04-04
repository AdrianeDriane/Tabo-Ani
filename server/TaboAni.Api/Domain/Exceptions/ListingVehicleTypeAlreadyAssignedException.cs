using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class ListingVehicleTypeAlreadyAssignedException : DomainException
{
    public ListingVehicleTypeAlreadyAssignedException(Guid listingId, Guid vehicleTypeId)
        : base(
            "listing_vehicle_type_already_assigned",
            $"Vehicle type '{vehicleTypeId}' is already assigned to listing '{listingId}'.",
            HttpStatusCode.Conflict)
    {
    }
}
