using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("carts");
        builder.ConfigureGuidKey(x => x.CartId);
        builder.ConfigureRequiredText(x => x.CartStatus).HasDefaultValue("ACTIVE");
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Cart>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
