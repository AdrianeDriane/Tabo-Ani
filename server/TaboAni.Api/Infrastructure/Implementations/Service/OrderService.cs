using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Exceptions;
using TaboAni.Api.Application.Extensions.MappingExtensions;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Infrastructure.Implementations.Service;

public sealed class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<OrderResponseDto> CreateOrderAsync(
        OrderRequestDto orderRequestDto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(orderRequestDto);

        var order = orderRequestDto.ToEntity();
        var now = DateTimeOffset.UtcNow;

        order.OrderId = Guid.NewGuid();
        order.CreatedAt = now;
        order.UpdatedAt = now;

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var createdOrder = await _unitOfWork.Orders.CreateOrderAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return createdOrder.ToResponseDto();
        }
        catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<OrderResponseDto> GetOrderByIdAsync(
        Guid orderId,
        CancellationToken cancellationToken = default)
    {
        ValidateOrderId(orderId);

        var order = await _unitOfWork.Orders.GetOrderByIdAsync(orderId, cancellationToken)
            ?? throw new OrderOperationException("Order not found.", OrderOperationFailureType.NotFound);

        return order.ToResponseDto();
    }

    public async Task<IEnumerable<OrderResponseDto>> GetOrdersByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User ID is required.", nameof(userId));
        }

        var orders = await _unitOfWork.Orders.GetOrdersByUserIdAsync(userId, cancellationToken);

        return orders.Select(order => order.ToResponseDto()).ToList();
    }

    public async Task<OrderResponseDto> PayDownpaymentAsync(
        Guid orderId,
        decimal amount,
        CancellationToken cancellationToken = default)
    {
        ValidateOrderId(orderId);

        return await ExecuteOrderMutationAsync(
            ct => _unitOfWork.Orders.PayDownpaymentAsync(orderId, amount, ct),
            cancellationToken);
    }

    public async Task<OrderResponseDto> PayFinalPaymentAsync(
        Guid orderId,
        decimal amount,
        CancellationToken cancellationToken = default)
    {
        ValidateOrderId(orderId);

        return await ExecuteOrderMutationAsync(
            ct => _unitOfWork.Orders.PayFinalPaymentAsync(orderId, amount, ct),
            cancellationToken);
    }

    public async Task<OrderResponseDto> CompleteOrderAsync(
        Guid orderId,
        CancellationToken cancellationToken = default)
    {
        ValidateOrderId(orderId);

        return await ExecuteOrderMutationAsync(
            ct => _unitOfWork.Orders.CompleteOrderAsync(orderId, ct),
            cancellationToken);
    }

    public async Task<OrderResponseDto> CancelOrderAsync(
        Guid orderId,
        CancellationToken cancellationToken = default)
    {
        ValidateOrderId(orderId);

        return await ExecuteOrderMutationAsync(
            ct => _unitOfWork.Orders.CancelOrderAsync(orderId, ct),
            cancellationToken);
    }

    private async Task<OrderResponseDto> ExecuteOrderMutationAsync(
        Func<CancellationToken, Task<Order>> operation,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var order = await operation(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return order.ToResponseDto();
        }
        catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static void ValidateOrderId(Guid orderId)
    {
        if (orderId == Guid.Empty)
        {
            throw new ArgumentException("Order ID is required.", nameof(orderId));
        }
    }
}
