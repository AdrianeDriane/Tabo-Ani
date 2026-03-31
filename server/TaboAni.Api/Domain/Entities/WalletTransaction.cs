namespace TaboAni.Api.Domain.Entities;

public class WalletTransaction
{
    public Guid WalletTransactionId { get; set; }
    public Guid WalletId { get; set; }
    public Guid? PaymentId { get; set; }
    public Guid? EscrowTransactionId { get; set; }
    public Guid? FarmerPayoutId { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal BalanceBefore { get; set; }
    public decimal BalanceAfter { get; set; }
    public string? ReferenceCode { get; set; }
    public string? Remarks { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

