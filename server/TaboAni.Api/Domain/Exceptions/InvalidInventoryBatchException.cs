using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class InvalidInventoryBatchException : DomainException
{
    public InvalidInventoryBatchException(string message) : base(
        "invalid_inventory_batch",
        message,
        HttpStatusCode.BadRequest)
    {
    }
}
