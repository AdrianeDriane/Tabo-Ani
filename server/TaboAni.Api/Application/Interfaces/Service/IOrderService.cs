using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.DTOs.Request;

namespace TaboAni.Api.Application.Interfaces.Service;

public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(OrderRequestDto orderRequestDto, CancellationToken cancellationToken = default);
    Task<OrderResponseDto> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderResponseDto>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<OrderResponseDto> PayDownpaymentAsync(Guid orderId, decimal amount, CancellationToken cancellationToken = default);
    Task<OrderResponseDto> PayFinalPaymentAsync(Guid orderId, decimal amount, CancellationToken cancellationToken = default);
    Task<OrderResponseDto> CompleteOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<OrderResponseDto> CancelOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
}
