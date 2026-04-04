namespace TaboAni.Api.Domain.Entities;

public class FarmerPayout
{
    public Guid FarmerPayoutId { get; set; }
    public Guid FarmerProfileId { get; set; }
    public Guid OrderId { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal PlatformFeeAmount { get; set; }
    public decimal NetAmount { get; set; }
    public string PayoutStatus { get; set; } = string.Empty;
    public DateTimeOffset? ReleasedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

