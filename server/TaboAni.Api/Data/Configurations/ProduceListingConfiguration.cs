using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Data.Configurations;

internal sealed class ProduceListingConfiguration : IEntityTypeConfiguration<ProduceListing>
{
    public void Configure(EntityTypeBuilder<ProduceListing> builder)
    {
        builder.ToTable("produce_listings");
        builder.ConfigureGuidKey(x => x.ProduceListingId);
        builder.ConfigureRequiredVarchar(x => x.ListingTitle, 150);
        builder.ConfigureRequiredVarchar(x => x.ProduceName, 150);
        builder.ConfigureOptionalText(x => x.Description);
        builder.ConfigureDecimal(x => x.PricePerKg, 12, 2);
        builder.ConfigureDecimal(x => x.MinimumOrderKg, 12, 3).HasDefaultValue(1.000m);
        builder.ConfigureOptionalDecimal(x => x.MaximumOrderKg, 12, 3);
        builder.Property(x => x.ListingStatus)
            .HasConversion(
                listingStatus => listingStatus.ToString().ToUpperInvariant(),
                listingStatus => Enum.Parse<ListingStatus>(listingStatus, true))
            .HasColumnType("text")
            .IsRequired();
        builder.ConfigureRequiredText(x => x.PrimaryLocationText);
        builder.ConfigureOptionalDecimal(x => x.PrimaryLatitude, 9, 6);
        builder.ConfigureOptionalDecimal(x => x.PrimaryLongitude, 9, 6);
        builder.Property(x => x.IsPremiumBoosted).HasDefaultValue(false);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasIndex(x => new { x.ProduceCategoryId, x.ListingStatus, x.CreatedAt })
            .HasDatabaseName("ix_produce_listings_category_status_created_at");
        builder.HasOne<FarmerProfile>()
            .WithMany()
            .HasForeignKey(x => x.FarmerProfileId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<ProduceCategory>()
            .WithMany()
            .HasForeignKey(x => x.ProduceCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

