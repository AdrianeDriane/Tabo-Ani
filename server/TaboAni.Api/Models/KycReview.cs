namespace TaboAni.Api.Models;

public class KycReview
{
    public Guid KycReviewId { get; set; }
    public Guid KycApplicationId { get; set; }
    public Guid ReviewedByUserId { get; set; }
    public string ReviewAction { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public DateTimeOffset ReviewedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
