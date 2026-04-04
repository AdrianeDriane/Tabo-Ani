namespace TaboAni.Api.Domain.Entities;

public class FarmerListingVehicleType
{
    public Guid ProduceListingId { get; set; }
    public Guid VehicleTypeId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

