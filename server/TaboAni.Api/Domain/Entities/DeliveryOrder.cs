namespace TaboAni.Api.Domain.Entities;

public class DeliveryOrder
{
    public Guid DeliveryOrderId { get; set; }
    public Guid DeliveryId { get; set; }
    public Guid OrderId { get; set; }
    public decimal ReservedCapacityKg { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

