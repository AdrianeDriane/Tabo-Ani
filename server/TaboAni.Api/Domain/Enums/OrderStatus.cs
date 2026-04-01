namespace TaboAni.Api.Domain.Enums;

public enum OrderStatus
{
    PendingDownpayment = 1,
    PendingFinalPayment = 2,
    Completed = 3,
    Cancelled = 4,
    ForDelivery = 5,
    Delivered = 6,
    InTransit = 7,
    Returned = 8,
    Refunded = 9
}