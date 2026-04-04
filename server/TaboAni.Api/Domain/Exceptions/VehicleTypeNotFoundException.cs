using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class VehicleTypeNotFoundException : DomainException
{
    public VehicleTypeNotFoundException(Guid vehicleTypeId)
        : base("vehicle_type_not_found", $"Vehicle type '{vehicleTypeId}' was not found.", HttpStatusCode.NotFound)
    {
    }
}
