namespace TaboAni.Api.Domain.Exceptions;

public sealed class InvalidListingException : DomainException
{
    public InvalidListingException(string message) : base("invalid_listing", message)
    {
    }
}
