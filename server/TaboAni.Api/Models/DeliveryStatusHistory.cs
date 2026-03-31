namespace TaboAni.Api.Models;

public class DeliveryStatusHistory
{
    public Guid DeliveryStatusHistoryId { get; set; }
    public Guid DeliveryId { get; set; }
    public Guid? TriggeredByUserId { get; set; }
    public string? FromStatus { get; set; }
    public string ToStatus { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
