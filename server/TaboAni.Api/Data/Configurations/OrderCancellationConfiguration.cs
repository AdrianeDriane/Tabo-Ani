using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class OrderCancellationConfiguration : IEntityTypeConfiguration<OrderCancellation>
{
    public void Configure(EntityTypeBuilder<OrderCancellation> builder)
    {
        builder.ToTable("order_cancellations");
        builder.ConfigureGuidKey(x => x.OrderCancellationId);
        builder.ConfigureRequiredVarchar(x => x.CancelledByRoleCode, 50);
        builder.ConfigureRequiredText(x => x.CancellationReason);
        builder.ConfigureTimestamp(x => x.CancelledAt).HasDefaultValueSql("now()");
        builder.ConfigureRequiredText(x => x.RefundPolicyApplied);
        builder.ConfigureDecimal(x => x.RefundPercentage, 5, 2).HasDefaultValue(0.00m);
        builder.ConfigureDecimal(x => x.RefundAmount, 12, 2).HasDefaultValue(0.00m);
        builder.ConfigureDecimal(x => x.FarmerKeptAmount, 12, 2).HasDefaultValue(0.00m);
        builder.ConfigureDecimal(x => x.PlatformKeptAmount, 12, 2).HasDefaultValue(0.00m);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasOne<Order>()
            .WithOne()
            .HasForeignKey<OrderCancellation>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CancelledByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

