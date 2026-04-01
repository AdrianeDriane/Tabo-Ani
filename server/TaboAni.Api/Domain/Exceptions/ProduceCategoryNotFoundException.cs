using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class ProduceCategoryNotFoundException : DomainException
{
    public ProduceCategoryNotFoundException(Guid produceCategoryId)
        : base("produce_category_not_found", $"Produce category '{produceCategoryId}' was not found.", HttpStatusCode.NotFound)
    {
    }
}
