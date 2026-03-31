using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
{
    public void Configure(EntityTypeBuilder<WalletTransaction> builder)
    {
        builder.ToTable("wallet_transactions");
        builder.ConfigureGuidKey(x => x.WalletTransactionId);
        builder.ConfigureRequiredText(x => x.TransactionType);
        builder.ConfigureDecimal(x => x.Amount, 12, 2);
        builder.ConfigureDecimal(x => x.BalanceBefore, 12, 2);
        builder.ConfigureDecimal(x => x.BalanceAfter, 12, 2);
        builder.ConfigureOptionalVarchar(x => x.ReferenceCode, 100);
        builder.ConfigureOptionalText(x => x.Remarks);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.WalletId, x.CreatedAt })
            .HasDatabaseName("ix_wallet_transactions_wallet_created_at");
        builder.HasOne<Wallet>()
            .WithMany()
            .HasForeignKey(x => x.WalletId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Payment>()
            .WithMany()
            .HasForeignKey(x => x.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<EscrowTransaction>()
            .WithMany()
            .HasForeignKey(x => x.EscrowTransactionId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<FarmerPayout>()
            .WithMany()
            .HasForeignKey(x => x.FarmerPayoutId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
