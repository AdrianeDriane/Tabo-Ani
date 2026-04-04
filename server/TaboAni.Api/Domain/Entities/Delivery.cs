namespace TaboAni.Api.Domain.Entities;

public class Delivery
{
    public Guid DeliveryId { get; set; }
    public Guid VehicleTypeId { get; set; }
    public string DeliveryCode { get; set; } = string.Empty;
    public string DeliveryStatus { get; set; } = string.Empty;
    public DateTimeOffset? PlannedPickupDate { get; set; }
    public DateTimeOffset? ActualPickupAt { get; set; }
    public DateTimeOffset? ActualArrivalAt { get; set; }
    public decimal TotalReservedCapacityKg { get; set; }
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

