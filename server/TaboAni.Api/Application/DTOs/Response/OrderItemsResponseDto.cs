namespace TaboAni.Api.Application.DTOs.Response;

public class OrderItemsResponseDto
{
    public Guid OrderId { get; set; }
    public Guid ProduceListingId { get; set; }
    public Guid FarmerProfileId { get; set; }
    public decimal QuantityKg { get; set; }
    public decimal UnitPricePerKg { get; set; }
    public decimal LineSubtotalAmount { get; set; }
    public string ListingTitleSnapshot { get; set; } = string.Empty;
    public string ProduceNameSnapshot { get; set; } = string.Empty;
}