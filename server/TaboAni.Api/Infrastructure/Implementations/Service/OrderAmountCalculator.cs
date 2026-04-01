using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Infrastructure.Implementations.Service;

public static class OrderAmountCalculator
{
    private const decimal DownpaymentRate = 0.5m;

    public static OrderAmounts Calculate(
        IEnumerable<OrderItem> orderItems,
        decimal deliveryFeeAmount = 0m,
        decimal platformFeeAmount = 0m,
        decimal refundAmount = 0m)
    {
        ArgumentNullException.ThrowIfNull(orderItems);

        var subtotalAmount = RoundCurrency(orderItems.Sum(orderItem => orderItem.LineSubtotalAmount));
        var totalAmount = RoundCurrency(subtotalAmount + deliveryFeeAmount + platformFeeAmount - refundAmount);
        var downpaymentDueAmount = RoundCurrency(totalAmount * DownpaymentRate);
        var finalPaymentDueAmount = RoundCurrency(totalAmount - downpaymentDueAmount);

        return new OrderAmounts(
            subtotalAmount,
            totalAmount,
            downpaymentDueAmount,
            finalPaymentDueAmount);
    }

    private static decimal RoundCurrency(decimal amount)
    {
        return decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
    }
}

public readonly record struct OrderAmounts(
    decimal SubtotalAmount,
    decimal TotalAmount,
    decimal DownpaymentDueAmount,
    decimal FinalPaymentDueAmount);
