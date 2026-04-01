using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");
        builder.ConfigureGuidKey(x => x.OrderItemId);
        builder.ConfigureDecimal(x => x.QuantityKg, 12, 3);
        builder.ConfigureDecimal(x => x.UnitPricePerKg, 12, 2);
        builder.ConfigureDecimal(x => x.LineSubtotalAmount, 12, 2);
        builder.ConfigureRequiredVarchar(x => x.ListingTitleSnapshot, 150);
        builder.ConfigureRequiredVarchar(x => x.ProduceNameSnapshot, 150);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasOne<Order>()
            .WithMany(order => order.OrderItems)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<ProduceListing>()
            .WithMany()
            .HasForeignKey(x => x.ProduceListingId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<FarmerProfile>()
            .WithMany()
            .HasForeignKey(x => x.FarmerProfileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

