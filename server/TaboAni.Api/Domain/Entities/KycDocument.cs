namespace TaboAni.Api.Domain.Entities;

public class KycDocument
{
    public Guid KycDocumentId { get; set; }
    public Guid KycApplicationId { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public long? FileSizeBytes { get; set; }
    public string DocumentStatus { get; set; } = string.Empty;
    public string? RejectionReason { get; set; }
    public DateTimeOffset UploadedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

