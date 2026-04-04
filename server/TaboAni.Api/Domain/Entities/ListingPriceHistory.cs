using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Domain.Entities;

public class ListingPriceHistory
{
    public Guid ListingPriceHistoryId { get; set; }
    public Guid ProduceListingId { get; set; }
    public decimal OldPricePerKg { get; set; }
    public decimal NewPricePerKg { get; set; }
    public Guid? ChangedByUserId { get; set; }
    public DateTimeOffset EffectiveAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public static ListingPriceHistory Create(
        Guid produceListingId,
        decimal oldPricePerKg,
        decimal newPricePerKg,
        DateTimeOffset effectiveAt,
        Guid? changedByUserId = null)
    {
        if (produceListingId == Guid.Empty)
        {
            throw new InvalidListingException("ProduceListingId is required.");
        }

        return new ListingPriceHistory
        {
            ListingPriceHistoryId = Guid.NewGuid(),
            ProduceListingId = produceListingId,
            OldPricePerKg = oldPricePerKg,
            NewPricePerKg = newPricePerKg,
            ChangedByUserId = changedByUserId,
            EffectiveAt = effectiveAt,
            CreatedAt = effectiveAt
        };
    }
}
