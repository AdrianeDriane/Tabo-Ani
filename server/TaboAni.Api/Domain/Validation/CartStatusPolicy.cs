namespace TaboAni.Api.Domain.Validation;

public static class CartStatusPolicy
{
    public const string Active = "ACTIVE";

    // TODO: Back cart statuses with a database constraint once non-active cart flows are introduced.
    public static bool IsActive(string cartStatus)
    {
        return string.Equals(cartStatus, Active, StringComparison.Ordinal);
    }
}
