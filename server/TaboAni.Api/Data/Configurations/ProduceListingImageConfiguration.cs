using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class ProduceListingImageConfiguration : IEntityTypeConfiguration<ProduceListingImage>
{
    public void Configure(EntityTypeBuilder<ProduceListingImage> builder)
    {
        builder.ToTable("produce_listing_images");
        builder.ConfigureGuidKey(x => x.ProduceListingImageId);
        builder.ConfigureRequiredText(x => x.ImageUrl);
        builder.Property(x => x.DisplayOrder).HasDefaultValue(1);
        builder.Property(x => x.IsPrimary).HasDefaultValue(false);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.ProduceListingId, x.DisplayOrder }).IsUnique();
        builder.HasOne<ProduceListing>()
            .WithMany()
            .HasForeignKey(x => x.ProduceListingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

