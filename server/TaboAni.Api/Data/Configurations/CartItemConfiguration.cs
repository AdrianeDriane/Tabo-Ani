using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("cart_items");
        builder.ConfigureGuidKey(x => x.CartItemId);
        builder.ConfigureDecimal(x => x.QuantityKg, 12, 3);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasIndex(x => new { x.CartId, x.ProduceListingId }).IsUnique();
        builder.HasOne<Cart>()
            .WithMany()
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<ProduceListing>()
            .WithMany()
            .HasForeignKey(x => x.ProduceListingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
