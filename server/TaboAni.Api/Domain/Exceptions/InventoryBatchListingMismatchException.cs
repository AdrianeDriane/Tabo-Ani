using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class InventoryBatchListingMismatchException : DomainException
{
    public InventoryBatchListingMismatchException(Guid batchId, Guid listingId) : base(
        "inventory_batch_listing_mismatch",
        $"Inventory batch '{batchId}' does not belong to listing '{listingId}'.",
        HttpStatusCode.BadRequest)
    {
    }
}
