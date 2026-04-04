namespace TaboAni.Api.Application.Validation.Order;

internal static class OrderValidationHelper
{
    public static Guid ValidateUserId(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User ID is required.", nameof(userId));
        }

        return userId;
    }
}
