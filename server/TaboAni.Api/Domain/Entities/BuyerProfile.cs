namespace TaboAni.Api.Domain.Entities;

public class BuyerProfile
{
    public Guid BuyerProfileId { get; set; }
    public Guid UserId { get; set; }
    public string BusinessName { get; set; } = string.Empty;
    public string ContactPersonName { get; set; } = string.Empty;
    public string BusinessType { get; set; } = string.Empty;
    public string BusinessLocationText { get; set; } = string.Empty;
    public decimal? BusinessLatitude { get; set; }
    public decimal? BusinessLongitude { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

