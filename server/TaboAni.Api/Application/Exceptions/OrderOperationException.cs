namespace TaboAni.Api.Application.Exceptions;

public enum OrderOperationFailureType
{
    Conflict = 1,
    NotFound = 2
}

public sealed class OrderOperationException(
    string message,
    OrderOperationFailureType failureType = OrderOperationFailureType.Conflict) : Exception(message)
{
    public OrderOperationFailureType FailureType { get; } = failureType;
}
