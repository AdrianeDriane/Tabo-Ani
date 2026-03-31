using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.DTOs.Request;

namespace TaboAni.Api.Application.Interfaces.Service;

public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(OrderRequestDto orderRequestDto, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderResponseDto>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}