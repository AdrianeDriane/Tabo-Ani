using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Extensions.MappingExtensions;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Service;

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

        if (order.OrderId == Guid.Empty)
        {
            order.OrderId = Guid.NewGuid();
        }

        if (order.CreatedAt == default)
        {
            order.CreatedAt = now;
        }

        order.UpdatedAt = order.UpdatedAt == default ? now : order.UpdatedAt;

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
}
