namespace TaboAni.Api.Models;

public class EscrowTransaction
{
    public Guid EscrowTransactionId { get; set; }
    public Guid OrderId { get; set; }
    public Guid? PaymentId { get; set; }
    public Guid? FarmerPayoutId { get; set; }
    public string EscrowAction { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string ActionStatus { get; set; } = string.Empty;
    public DateTimeOffset ActedAt { get; set; }
    public string? Remarks { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
