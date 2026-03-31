using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class EscrowTransactionConfiguration : IEntityTypeConfiguration<EscrowTransaction>
{
    public void Configure(EntityTypeBuilder<EscrowTransaction> builder)
    {
        builder.ToTable("escrow_transactions");
        builder.ConfigureGuidKey(x => x.EscrowTransactionId);
        builder.ConfigureRequiredText(x => x.EscrowAction);
        builder.ConfigureDecimal(x => x.Amount, 12, 2);
        builder.ConfigureRequiredText(x => x.ActionStatus);
        builder.ConfigureTimestamp(x => x.ActedAt).HasDefaultValueSql("now()");
        builder.ConfigureOptionalText(x => x.Remarks);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Payment>()
            .WithMany()
            .HasForeignKey(x => x.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<FarmerPayout>()
            .WithMany()
            .HasForeignKey(x => x.FarmerPayoutId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

