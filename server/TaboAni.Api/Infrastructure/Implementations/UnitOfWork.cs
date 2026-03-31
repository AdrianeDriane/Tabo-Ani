using Microsoft.EntityFrameworkCore.Storage;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Data;

namespace TaboAni.Api.Infrastructure.Implementations;

public sealed class UnitOfWork(AppDbContext context, IOrderRepository orderRepository) : IUnitOfWork, IAsyncDisposable
{
    private readonly AppDbContext _context = context;
    private IDbContextTransaction? _currentTransaction;

    public IOrderRepository Orders { get; } = orderRepository;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is not null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is null)
        {
            throw new InvalidOperationException("No active transaction to commit.");
        }

        try
        {
            await _currentTransaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is null)
        {
            throw new InvalidOperationException("No active transaction to roll back.");
        }

        try
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
            _context.ChangeTracker.Clear();
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        if (_currentTransaction is null)
        {
            return;
        }

        await _currentTransaction.DisposeAsync();
        _currentTransaction = null;
    }
}
