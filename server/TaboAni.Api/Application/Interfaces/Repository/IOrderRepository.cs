using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Application.Interfaces.Repository;

public interface IOrderRepository
{
    Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task<bool> OrderNumberExistsAsync(string orderNumber, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Order?> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<Order> PayDownpaymentAsync(Guid orderId, decimal amount, CancellationToken cancellationToken = default);
    Task<Order> PayFinalPaymentAsync(Guid orderId, decimal amount, CancellationToken cancellationToken = default);
    Task<Order> CompleteOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<Order> CancelOrderAsync(Guid orderId, CancellationToken cancellationToken = default);
}
