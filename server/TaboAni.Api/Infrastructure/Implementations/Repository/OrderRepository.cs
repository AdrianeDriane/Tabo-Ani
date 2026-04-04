using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Data;

namespace TaboAni.Api.Infrastructure.Implementations.Repository;

public sealed class OrderRepository(AppDbContext context) : IOrderRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Where(order => order.BuyerUserId == userId)
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
