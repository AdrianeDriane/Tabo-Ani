namespace TaboAni.Api.Application.DTOs.Request;

public class OrderItemsRequestDto
{
    public Guid ProduceListingId { get; set; }
    public Guid FarmerProfileId { get; set; }
    public decimal QuantityKg { get; set; }
}