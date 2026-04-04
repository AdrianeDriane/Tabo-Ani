using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class InventoryBatchNotFoundException : DomainException
{
    public InventoryBatchNotFoundException(Guid batchId) : base(
        "inventory_batch_not_found",
        $"Inventory batch '{batchId}' was not found.",
        HttpStatusCode.NotFound)
    {
    }
}
