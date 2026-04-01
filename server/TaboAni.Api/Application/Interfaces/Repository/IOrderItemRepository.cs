using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Application.Interfaces.Repository;

public interface IOrderItemRepository
{
    Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderItem>> CreateBulkOrderItemsAsync(IEnumerable<OrderItem> orderItems, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<OrderItem?> GetOrderItemByIdAsync(Guid orderItemId, CancellationToken cancellationToken = default);
    Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem, CancellationToken cancellationToken = default);
    Task DeleteOrderItemAsync(Guid orderItemId, CancellationToken cancellationToken = default);
}