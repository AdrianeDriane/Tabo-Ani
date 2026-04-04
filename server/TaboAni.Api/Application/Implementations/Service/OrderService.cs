using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Extensions.MappingExtensions;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Application.Common;
using TaboAni.Api.Application.Validation.Order;

namespace TaboAni.Api.Application.Implementations.Service;

public sealed class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<OrderResponseDto> CreateOrderAsync(
        OrderRequestDto orderRequestDto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(orderRequestDto);

        var order = orderRequestDto.ToEntity();
        order.EnsureIdentityAndTimestamps(DateTimeOffset.UtcNow);

        OrderResponseDto response = null!;

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            var createdOrder = await _unitOfWork.Orders.CreateOrderAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            response = createdOrder.ToResponseDto();
        }, cancellationToken);

        return response;
    }

    public async Task<IEnumerable<OrderResponseDto>> GetOrdersByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var validatedUserId = OrderValidationHelper.ValidateUserId(userId);
        var orders = await _unitOfWork.Orders.GetOrdersByUserIdAsync(validatedUserId, cancellationToken);

        return orders.Select(order => order.ToResponseDto()).ToList();
    }
}
