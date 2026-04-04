using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Data.Configurations;

internal sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("carts");
        builder.ConfigureGuidKey(x => x.CartId);
        builder.Property(x => x.CartStatus)
            .HasConversion(
                cartStatus => cartStatus.ToString().ToUpperInvariant(),
                cartStatus => Enum.Parse<CartStatus>(cartStatus, true))
            .HasColumnType("text")
            .IsRequired()
            .HasDefaultValue(CartStatus.Active);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Cart>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
