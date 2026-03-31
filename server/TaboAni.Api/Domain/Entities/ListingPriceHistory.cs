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
}

