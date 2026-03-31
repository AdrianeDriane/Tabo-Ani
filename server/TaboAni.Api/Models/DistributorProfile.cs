namespace TaboAni.Api.Models;

public class DistributorProfile
{
    public Guid DistributorProfileId { get; set; }
    public Guid UserId { get; set; }
    public string FleetDisplayName { get; set; } = string.Empty;
    public string? LicenseNumber { get; set; }
    public string BaseLocationText { get; set; } = string.Empty;
    public decimal? BaseLatitude { get; set; }
    public decimal? BaseLongitude { get; set; }
    public bool IsAvailable { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
