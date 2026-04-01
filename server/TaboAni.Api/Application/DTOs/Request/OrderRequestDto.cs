using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Application.DTOs.Request;

public class OrderRequestDto
{
    public Guid BuyerUserId { get; set; }
    public OrderStatus OrderStatus { get; set; }
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
    public DateTimeOffset? FinalPaymentPaidAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public DateTimeOffset? CancelledAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }   
    public List<OrderItemsRequestDto> OrderItems { get; set; } = new();
}

public class InitialOrderRequestDto
{
    public Guid BuyerUserId { get; set; }
    public string DeliveryLocationText { get; set; } = string.Empty;
    public decimal? DeliveryLatitude { get; set; }
    public decimal? DeliveryLongitude { get; set; }
    public DateOnly? RequestedDeliveryDate { get; set; }
    public List<OrderItemsRequestDto> OrderItems { get; set; } = new();
}

public sealed class OrderPaymentRequestDto
{
    public decimal Amount { get; set; }
}
