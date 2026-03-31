namespace TaboAni.Api.Domain.Entities;

public class Review
{
    public Guid ReviewId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProduceListingId { get; set; }
    public Guid ReviewerUserId { get; set; }
    public short StarRating { get; set; }
    public string? ReviewText { get; set; }
    public string ReviewStatus { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

