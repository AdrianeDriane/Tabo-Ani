using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Domain.Policy;

public static class CartStatusPolicy
{
    public static bool IsActive(CartStatus status) => status == CartStatus.Active;
}
