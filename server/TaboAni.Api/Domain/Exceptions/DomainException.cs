using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string code, string message, HttpStatusCode statusCode) : base(message)
    {
        Code = code;
        StatusCode = statusCode;
    }

    public string Code { get; }
    public HttpStatusCode StatusCode { get; }
}
