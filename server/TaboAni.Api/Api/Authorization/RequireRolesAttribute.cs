using Microsoft.AspNetCore.Authorization;

namespace TaboAni.Api.Api.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public sealed class RequireRolesAttribute : AuthorizeAttribute
{
    public RequireRolesAttribute(params string[] allowedRoles)
    {
        Roles = string.Join(
            ",",
            allowedRoles
                .Where(role => !string.IsNullOrWhiteSpace(role))
                .Select(role => role.Trim())
                .Distinct(StringComparer.Ordinal));
    }
}
