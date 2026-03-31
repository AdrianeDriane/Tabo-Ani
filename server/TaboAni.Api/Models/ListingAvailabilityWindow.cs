namespace TaboAni.Api.Models;

public class ListingAvailabilityWindow
{
    public Guid ListingAvailabilityWindowId { get; set; }
    public Guid ProduceListingId { get; set; }
    public DateOnly AvailableFromDate { get; set; }
    public DateOnly AvailableToDate { get; set; }
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
