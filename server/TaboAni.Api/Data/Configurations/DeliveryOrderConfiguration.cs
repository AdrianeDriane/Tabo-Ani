using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class DeliveryOrderConfiguration : IEntityTypeConfiguration<DeliveryOrder>
{
    public void Configure(EntityTypeBuilder<DeliveryOrder> builder)
    {
        builder.ToTable("delivery_orders");
        builder.ConfigureGuidKey(x => x.DeliveryOrderId);
        builder.ConfigureDecimal(x => x.ReservedCapacityKg, 12, 3);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.DeliveryId, x.OrderId }).IsUnique();
        builder.HasIndex(x => x.OrderId)
            .IsUnique()
            .HasDatabaseName("uq_delivery_orders_order_id");
        builder.HasOne<Delivery>()
            .WithMany()
            .HasForeignKey(x => x.DeliveryId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
