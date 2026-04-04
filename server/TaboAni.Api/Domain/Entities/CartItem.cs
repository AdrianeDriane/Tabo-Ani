using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Domain.Entities;

public class CartItem
{
    public Guid CartItemId { get; set; }
    public Guid CartId { get; set; }
    public Guid ProduceListingId { get; set; }
    public decimal QuantityKg { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public static CartItem Create(
        Guid cartId,
        Guid produceListingId,
        decimal quantityKg,
        DateTimeOffset now)
    {
        if (cartId == Guid.Empty)
        {
            throw new InvalidCartException("CartId is required.");
        }

        if (produceListingId == Guid.Empty)
        {
            throw new InvalidCartException("ProduceListingId is required.");
        }

        EnsureQuantity(quantityKg);

        return new CartItem
        {
            CartItemId = Guid.NewGuid(),
            CartId = cartId,
            ProduceListingId = produceListingId,
            QuantityKg = quantityKg,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    public decimal IncreaseQuantity(decimal addedQuantityKg, DateTimeOffset updatedAt)
    {
        EnsureQuantity(addedQuantityKg);
        QuantityKg += addedQuantityKg;
        UpdatedAt = updatedAt;
        return QuantityKg;
    }

    public void SetQuantity(decimal quantityKg, DateTimeOffset updatedAt)
    {
        EnsureQuantity(quantityKg);
        QuantityKg = quantityKg;
        UpdatedAt = updatedAt;
    }

    private static void EnsureQuantity(decimal quantityKg)
    {
        if (quantityKg <= 0)
        {
            throw new InvalidCartException("QuantityKg must be greater than 0.");
        }
    }
}
