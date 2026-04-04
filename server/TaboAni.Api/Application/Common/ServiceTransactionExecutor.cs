using TaboAni.Api.Application.Interfaces.Repository;

namespace TaboAni.Api.Application.Common;

internal static class ServiceTransactionExecutor
{
    public static async Task ExecuteWithinTransactionAsync(
        IUnitOfWork unitOfWork,
        Func<Task> action,
        CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            await action();
            await unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
