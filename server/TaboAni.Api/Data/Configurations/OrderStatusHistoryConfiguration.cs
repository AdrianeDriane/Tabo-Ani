using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<OrderStatusHistory>
{
    public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
    {
        builder.ToTable("order_status_history");
        builder.ConfigureGuidKey(x => x.OrderStatusHistoryId);
        builder.ConfigureOptionalText(x => x.FromStatus);
        builder.ConfigureRequiredText(x => x.ToStatus);
        builder.ConfigureOptionalText(x => x.Remarks);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.OrderId, x.CreatedAt })
            .HasDatabaseName("ix_order_status_history_order_created_at");
        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.TriggeredByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
