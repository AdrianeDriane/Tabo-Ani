namespace TaboAni.Api.Application.DTOs.Response;

public class OrderResponseDto
{
    public Guid OrderId { get; set; }
    public Guid BuyerUserId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string OrderStatus { get; set; } = string.Empty;
    public decimal DownpaymentDueAmount { get; set; }
    public decimal DownpaymentPaidAmount { get; set; }
    public decimal FinalPaymentDueAmount { get; set; }
    public decimal FinalPaymentPaidAmount { get; set; }
    public decimal SubtotalAmount { get; set; }
    public decimal DeliveryFeeAmount { get; set; }
    public decimal PlatformFeeAmount { get; set; }
    public decimal RefundAmount { get; set; }
    public string DeliveryLocationText { get; set; } = string.Empty;
    public decimal? DeliveryLatitude { get; set; }
    public decimal? DeliveryLongitude { get; set; }
    public DateOnly? RequestedDeliveryDate { get; set; }
    public DateTimeOffset? DownpaymentPaidAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public DateTimeOffset? CancelledAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
