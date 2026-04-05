using System.Net;

namespace TaboAni.Api.Domain.Exceptions;

public sealed class AccountStatusNotAllowedException(string accountStatus)
    : DomainException(
        "auth.account_status_not_allowed",
        $"Account status '{accountStatus}' is not allowed to authenticate.",
        HttpStatusCode.Forbidden)
{
    public string AccountStatus { get; } = accountStatus;
}
