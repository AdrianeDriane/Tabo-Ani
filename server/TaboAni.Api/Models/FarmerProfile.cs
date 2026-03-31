namespace TaboAni.Api.Models;

public class FarmerProfile
{
    public Guid FarmerProfileId { get; set; }
    public Guid UserId { get; set; }
    public string FarmName { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string FarmLocationText { get; set; } = string.Empty;
    public decimal? FarmLatitude { get; set; }
    public decimal? FarmLongitude { get; set; }
    public int? YearsOfExperience { get; set; }
    public bool IsPublic { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
