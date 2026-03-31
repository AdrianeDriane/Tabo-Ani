namespace TaboAni.Api.Models;

public class OrderStatusHistory
{
    public Guid OrderStatusHistoryId { get; set; }
    public Guid OrderId { get; set; }
    public Guid? TriggeredByUserId { get; set; }
    public string? FromStatus { get; set; }
    public string ToStatus { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
