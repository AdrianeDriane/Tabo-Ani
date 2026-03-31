namespace TaboAni.Api.Models;

public class ProduceListing
{
    public Guid ProduceListingId { get; set; }
    public Guid FarmerProfileId { get; set; }
    public Guid ProduceCategoryId { get; set; }
    public string ListingTitle { get; set; } = string.Empty;
    public string ProduceName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal PricePerKg { get; set; }
    public decimal MinimumOrderKg { get; set; }
    public decimal? MaximumOrderKg { get; set; }
    public string ListingStatus { get; set; } = string.Empty;
    public string PrimaryLocationText { get; set; } = string.Empty;
    public decimal? PrimaryLatitude { get; set; }
    public decimal? PrimaryLongitude { get; set; }
    public bool IsPremiumBoosted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
