using TaboAni.Api.Domain.Exceptions;
using TaboAni.Api.Domain.Enums;
using TaboAni.Api.Domain.Policy;

namespace TaboAni.Api.Domain.Entities;

public class Cart
{
    public Guid CartId { get; set; }
    public Guid UserId { get; set; }
    public CartStatus CartStatus { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public static Cart CreateActive(Guid userId, DateTimeOffset now)
    {
        if (userId == Guid.Empty)
        {
            throw new InvalidCartException("UserId is required.");
        }

        return new Cart
        {
            CartId = Guid.NewGuid(),
            UserId = userId,
            CartStatus = CartStatus.Active,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    public void EnsureActive()
    {
        if (!CartStatusPolicy.IsActive(CartStatus))
        {
            throw new CartIntegrityException("Only a single ACTIVE cart is supported for each user.");
        }
    }

    public void Touch(DateTimeOffset updatedAt)
    {
        UpdatedAt = updatedAt;
    }
}
