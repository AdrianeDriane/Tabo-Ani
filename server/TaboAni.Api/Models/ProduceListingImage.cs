namespace TaboAni.Api.Models;

public class ProduceListingImage
{
    public Guid ProduceListingImageId { get; set; }
    public Guid ProduceListingId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsPrimary { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
