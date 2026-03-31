using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.ConfigureGuidKey(x => x.OrderId);
        builder.ConfigureRequiredVarchar(x => x.OrderNumber, 50);
        builder.ConfigureRequiredText(x => x.OrderStatus);
        builder.ConfigureDecimal(x => x.DownpaymentDueAmount, 12, 2);
        builder.ConfigureDecimal(x => x.DownpaymentPaidAmount, 12, 2).HasDefaultValue(0.00m);
        builder.ConfigureDecimal(x => x.FinalPaymentDueAmount, 12, 2);
        builder.ConfigureDecimal(x => x.FinalPaymentPaidAmount, 12, 2).HasDefaultValue(0.00m);
        builder.ConfigureDecimal(x => x.SubtotalAmount, 12, 2);
        builder.ConfigureDecimal(x => x.DeliveryFeeAmount, 12, 2).HasDefaultValue(0.00m);
        builder.ConfigureDecimal(x => x.PlatformFeeAmount, 12, 2).HasDefaultValue(0.00m);
        builder.ConfigureDecimal(x => x.RefundAmount, 12, 2).HasDefaultValue(0.00m);
        builder.ConfigureRequiredText(x => x.DeliveryLocationText);
        builder.ConfigureOptionalDecimal(x => x.DeliveryLatitude, 9, 6);
        builder.ConfigureOptionalDecimal(x => x.DeliveryLongitude, 9, 6);
        builder.ConfigureOptionalDate(x => x.RequestedDeliveryDate);
        builder.ConfigureOptionalTimestamp(x => x.DownpaymentPaidAt);
        builder.ConfigureOptionalTimestamp(x => x.CompletedAt);
        builder.ConfigureOptionalTimestamp(x => x.CancelledAt);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasIndex(x => x.OrderNumber).IsUnique();
        builder.HasIndex(x => new { x.BuyerUserId, x.OrderStatus, x.CreatedAt })
            .HasDatabaseName("ix_orders_buyer_status_created_at");
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.BuyerUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
