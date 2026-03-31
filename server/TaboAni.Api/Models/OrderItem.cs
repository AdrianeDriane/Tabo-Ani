namespace TaboAni.Api.Models;

public class OrderItem
{
    public Guid OrderItemId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProduceListingId { get; set; }
    public decimal QuantityKg { get; set; }
    public decimal UnitPricePerKg { get; set; }
    public decimal LineSubtotalAmount { get; set; }
    public string ListingTitleSnapshot { get; set; } = string.Empty;
    public string ProduceNameSnapshot { get; set; } = string.Empty;
    public Guid FarmerProfileId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
