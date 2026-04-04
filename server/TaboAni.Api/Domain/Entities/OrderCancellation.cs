namespace TaboAni.Api.Domain.Entities;

public class OrderCancellation
{
    public Guid OrderCancellationId { get; set; }
    public Guid OrderId { get; set; }
    public Guid CancelledByUserId { get; set; }
    public string CancelledByRoleCode { get; set; } = string.Empty;
    public string CancellationReason { get; set; } = string.Empty;
    public DateTimeOffset CancelledAt { get; set; }
    public string RefundPolicyApplied { get; set; } = string.Empty;
    public decimal RefundPercentage { get; set; }
    public decimal RefundAmount { get; set; }
    public decimal FarmerKeptAmount { get; set; }
    public decimal PlatformKeptAmount { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

