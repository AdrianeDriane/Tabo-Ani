using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Domain.Entities;

public class FarmerListingVehicleType
{
    public Guid ProduceListingId { get; set; }
    public Guid VehicleTypeId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public ProduceListing ProduceListing { get; set; } = null!;
    public VehicleType VehicleType { get; set; } = null!;

    public static FarmerListingVehicleType Create(Guid produceListingId, Guid vehicleTypeId, DateTimeOffset createdAt)
    {
        if (produceListingId == Guid.Empty)
        {
            throw new InvalidListingException("ProduceListingId is required.");
        }

        if (vehicleTypeId == Guid.Empty)
        {
            throw new InvalidListingException("VehicleTypeId is required.");
        }

        return new FarmerListingVehicleType
        {
            ProduceListingId = produceListingId,
            VehicleTypeId = vehicleTypeId,
            CreatedAt = createdAt
        };
    }
}
