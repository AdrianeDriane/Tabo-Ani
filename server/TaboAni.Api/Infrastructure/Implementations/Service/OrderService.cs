using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Exceptions;
using TaboAni.Api.Application.Extensions.MappingExtensions;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Infrastructure.Implementations.Service;

public sealed class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<OrderResponseDto> CreateOrderAsync(
        InitialOrderRequestDto orderRequestDto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(orderRequestDto);

        var orderItems = CreateOrderItems(orderRequestDto.OrderItems);
        var order = CreateOrder(orderRequestDto);
        var now = DateTimeOffset.UtcNow;

        InitializeOrder(order, now);
        AssignOrderId(orderItems, order.OrderId);

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            await _unitOfWork.Orders.CreateOrderAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (orderItems.Count > 0)
            {
                orderItems.ForEach(orderItem => orderItem.OrderId = order.OrderId);
                await _unitOfWork.OrderItems.CreateBulkOrderItemsAsync(orderItems, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var createdOrder = await _unitOfWork.Orders.GetOrderByIdAsync(order.OrderId, cancellationToken)
                ?? throw new OrderOperationException("Order not found after creation.", OrderOperationFailureType.NotFound);
            await _unitOfWork.CommitAsync(cancellationToken);

            return createdOrder.ToResponseDto();
        }
        catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static Order CreateOrder(InitialOrderRequestDto orderRequestDto)
    {
        var order = orderRequestDto.ToEntity();
        order.OrderItems = [];

        return order;
    }

    private static List<OrderItem> CreateOrderItems(IEnumerable<OrderItemsRequestDto>? orderItems)
    {
        if (orderItems is null)
        {
            return [];
        }

        return orderItems
            .Select(orderItem => orderItem.ToEntity())
            .ToList();
    }

    private static void InitializeOrder(Order order, DateTimeOffset now)
    {
        order.OrderId = Guid.NewGuid();
        order.OrderStatus = OrderStatus.PendingDownpayment;
        order.CreatedAt = now;
        order.UpdatedAt = now;
    }

    private static void AssignOrderId(IEnumerable<OrderItem> orderItems, Guid orderId)
    {
        foreach (var orderItem in orderItems)
        {
            orderItem.OrderId = orderId;
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
