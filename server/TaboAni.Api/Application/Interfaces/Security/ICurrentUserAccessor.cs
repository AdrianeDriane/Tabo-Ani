namespace TaboAni.Api.Application.Interfaces.Security;

public interface ICurrentUserAccessor
{
    bool IsAuthenticated { get; }
    Guid GetRequiredUserId();
    IReadOnlyList<string> GetRoleCodes();
}
