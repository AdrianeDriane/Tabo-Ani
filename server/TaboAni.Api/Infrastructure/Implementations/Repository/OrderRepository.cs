using TaboAni.Api.Application.Exceptions;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Data;
using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Infrastructure.Implementations.Repository;

public sealed class OrderRepository(AppDbContext context) : IOrderRepository
{
    private readonly AppDbContext _context = context;

    // Creation
    public async Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);

        return order;
    }

    // Retrievals
    public async Task<Order?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(order => order.OrderId == orderId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(order => order.BuyerUserId == userId)
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order> CancelOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        var affectedRows = await _context.Orders
            .Where(order =>
                order.OrderId == orderId &&
                order.OrderStatus != OrderStatus.Cancelled &&
                order.OrderStatus != OrderStatus.Completed &&
                order.CreatedAt >= now.AddDays(-3))
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(order => order.OrderStatus, _ => OrderStatus.Cancelled)
                .SetProperty(order => order.CancelledAt, _ => (DateTimeOffset?)now)
                .SetProperty(order => order.UpdatedAt, _ => now), cancellationToken);

        if (affectedRows == 0)
        {
            await ThrowCancelOrderFailureAsync(orderId, now, cancellationToken);
        }

        return await GetRequiredOrderByIdAsync(orderId, cancellationToken);
    }

    public async Task<Order> CompleteOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        var affectedRows = await _context.Orders
            .Where(order =>
                order.OrderId == orderId &&
                (order.OrderStatus == OrderStatus.PendingDownpayment || order.OrderStatus == OrderStatus.PendingFinalPayment) &&
                order.DownpaymentDueAmount <= 0 &&
                order.FinalPaymentDueAmount <= 0)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(order => order.OrderStatus, _ => OrderStatus.Completed)
                .SetProperty(order => order.CompletedAt, _ => (DateTimeOffset?)now)
                .SetProperty(order => order.UpdatedAt, _ => now), cancellationToken);

        if (affectedRows == 0)
        {
            await ThrowCompleteOrderFailureAsync(orderId, cancellationToken);
        }

        return await GetRequiredOrderByIdAsync(orderId, cancellationToken);
    }

    public async Task<Order> PayDownpaymentAsync(Guid orderId, decimal amount, CancellationToken cancellationToken = default)
    {
        ValidatePaymentAmount(amount);

        var now = DateTimeOffset.UtcNow;
        var affectedRows = await _context.Orders
            .Where(order =>
                order.OrderId == orderId &&
                order.OrderStatus == OrderStatus.PendingDownpayment &&
                order.DownpaymentDueAmount >= amount)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(order => order.DownpaymentPaidAmount, order => order.DownpaymentPaidAmount + amount)
                .SetProperty(order => order.DownpaymentDueAmount, order => order.DownpaymentDueAmount - amount)
                .SetProperty(
                    order => order.OrderStatus,
                    order => order.DownpaymentDueAmount == amount
                        ? OrderStatus.PendingFinalPayment
                        : OrderStatus.PendingDownpayment)
                .SetProperty(order => order.DownpaymentPaidAt, _ => (DateTimeOffset?)now)
                .SetProperty(order => order.UpdatedAt, _ => now), cancellationToken);

        if (affectedRows == 0)
        {
            await ThrowDownpaymentFailureAsync(orderId, amount, cancellationToken);
        }

        return await GetRequiredOrderByIdAsync(orderId, cancellationToken);
    }

    public async Task<Order> PayFinalPaymentAsync(Guid orderId, decimal amount, CancellationToken cancellationToken = default)
    {
        ValidatePaymentAmount(amount);

        var now = DateTimeOffset.UtcNow;
        var affectedRows = await _context.Orders
            .Where(order =>
                order.OrderId == orderId &&
                order.OrderStatus == OrderStatus.PendingFinalPayment &&
                order.FinalPaymentDueAmount >= amount)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(order => order.FinalPaymentPaidAmount, order => order.FinalPaymentPaidAmount + amount)
                .SetProperty(order => order.FinalPaymentDueAmount, order => order.FinalPaymentDueAmount - amount)
                .SetProperty(order => order.FinalPaymentPaidAt, _ => (DateTimeOffset?)now)
                .SetProperty(order => order.UpdatedAt, _ => now), cancellationToken);

        if (affectedRows == 0)
        {
            await ThrowFinalPaymentFailureAsync(orderId, amount, cancellationToken);
        }

        return await GetRequiredOrderByIdAsync(orderId, cancellationToken);
    }

    private async Task<Order> GetRequiredOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return await GetOrderByIdAsync(orderId, cancellationToken)
            ?? throw new OrderOperationException("Order not found.", OrderOperationFailureType.NotFound);
    }

    private static void ValidatePaymentAmount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Payment amount must be greater than zero.", nameof(amount));
        }

        if (decimal.Round(amount, 2) != amount)
        {
            throw new ArgumentException("Payment amount cannot have more than 2 decimal places.", nameof(amount));
        }
    }

    private async Task ThrowCancelOrderFailureAsync(
        Guid orderId,
        DateTimeOffset now,
        CancellationToken cancellationToken)
    {
        var order = await GetRequiredOrderByIdAsync(orderId, cancellationToken);

        if (order.OrderStatus == OrderStatus.Cancelled)
        {
            throw new OrderOperationException("Order is already cancelled.");
        }

        if (order.OrderStatus == OrderStatus.Completed)
        {
            throw new OrderOperationException("Completed orders cannot be cancelled.");
        }

        if (now > order.CreatedAt.AddDays(3))
        {
            throw new OrderOperationException("Order cannot be cancelled after 3 days of creation.");
        }

        throw new OrderOperationException("Order could not be cancelled.");
    }

    private async Task ThrowCompleteOrderFailureAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await GetRequiredOrderByIdAsync(orderId, cancellationToken);

        if (order.OrderStatus == OrderStatus.Completed)
        {
            throw new OrderOperationException("Order is already completed.");
        }

        if (order.OrderStatus != OrderStatus.PendingDownpayment && order.OrderStatus != OrderStatus.PendingFinalPayment)
        {
            throw new OrderOperationException("Only orders pending final payment or downpayment can be completed.");
        }

        if (order.DownpaymentDueAmount > 0 || order.FinalPaymentDueAmount > 0)
        {
            throw new OrderOperationException("Order cannot be completed while there are still due amounts.");
        }

        throw new OrderOperationException("Order could not be completed.");
    }

    private async Task ThrowDownpaymentFailureAsync(
        Guid orderId,
        decimal amount,
        CancellationToken cancellationToken)
    {
        var order = await GetRequiredOrderByIdAsync(orderId, cancellationToken);

        if (order.OrderStatus != OrderStatus.PendingDownpayment)
        {
            throw new OrderOperationException("Downpayment can only be paid for orders pending downpayment.");
        }

        if (amount > order.DownpaymentDueAmount)
        {
            throw new OrderOperationException("Payment amount cannot exceed the downpayment due amount.");
        }

        throw new OrderOperationException("Downpayment could not be applied.");
    }

    private async Task ThrowFinalPaymentFailureAsync(
        Guid orderId,
        decimal amount,
        CancellationToken cancellationToken)
    {
        var order = await GetRequiredOrderByIdAsync(orderId, cancellationToken);

        if (order.OrderStatus != OrderStatus.PendingFinalPayment)
        {
            throw new OrderOperationException("Final payment can only be paid for orders pending final payment.");
        }

        if (amount > order.FinalPaymentDueAmount)
        {
            throw new OrderOperationException("Payment amount cannot exceed the final payment due amount.");
        }

        throw new OrderOperationException("Final payment could not be applied.");
    }
}
