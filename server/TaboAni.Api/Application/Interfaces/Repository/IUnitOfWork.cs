namespace TaboAni.Api.Application.Interfaces.Repository;

public interface IUnitOfWork
{
    IOrderRepository Orders { get; }
    IMarketplaceRepository Marketplace { get; }
    ICartRepository Cart { get; }
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
