namespace TaboAni.Api.Domain.Entities;

public class QaReportImage
{
    public Guid QaReportImageId { get; set; }
    public Guid QaReportId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

