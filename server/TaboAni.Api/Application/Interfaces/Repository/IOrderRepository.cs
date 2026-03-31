using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Application.Interfaces.Repository;

public interface IOrderRepository
{
    Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}