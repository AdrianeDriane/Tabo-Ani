namespace TaboAni.Api.Models;

public class FarmerListingVehicleType
{
    public Guid FarmerListingVehicleTypeId { get; set; }
    public Guid ProduceListingId { get; set; }
    public Guid VehicleTypeId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
