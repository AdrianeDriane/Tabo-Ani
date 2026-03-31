namespace TaboAni.Api.Models;

public class VehicleType
{
    public Guid VehicleTypeId { get; set; }
    public string VehicleTypeName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal MaxCapacityKg { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
