using System.Security.Claims;
using TaboAni.Api.Application.Interfaces.Security;

namespace TaboAni.Api.Application.Security;

public sealed class HttpContextCurrentUserAccessor(IHttpContextAccessor httpContextAccessor) : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true;

    public Guid GetRequiredUserId()
    {
        var principal = _httpContextAccessor.HttpContext?.User;
        var userIdValue = principal?.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? principal?.FindFirstValue("sub");

        if (!Guid.TryParse(userIdValue, out var userId))
        {
            throw new InvalidOperationException("The authenticated user context does not contain a valid user ID.");
        }

        return userId;
    }

    public IReadOnlyList<string> GetRoleCodes()
    {
        var principal = _httpContextAccessor.HttpContext?.User;

        return principal?.FindAll(ClaimTypes.Role)
            .Select(claim => claim.Value)
            .Distinct(StringComparer.Ordinal)
            .ToArray()
            ?? Array.Empty<string>();
    }
}
