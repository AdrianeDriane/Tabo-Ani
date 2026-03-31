using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class ListingPriceHistoryConfiguration : IEntityTypeConfiguration<ListingPriceHistory>
{
    public void Configure(EntityTypeBuilder<ListingPriceHistory> builder)
    {
        builder.ToTable("listing_price_history");
        builder.ConfigureGuidKey(x => x.ListingPriceHistoryId);
        builder.ConfigureDecimal(x => x.OldPricePerKg, 12, 2);
        builder.ConfigureDecimal(x => x.NewPricePerKg, 12, 2);
        builder.ConfigureTimestamp(x => x.EffectiveAt).HasDefaultValueSql("now()");
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.ProduceListingId, x.EffectiveAt })
            .HasDatabaseName("ix_listing_price_history_listing_effective_at");
        builder.HasOne<ProduceListing>()
            .WithMany()
            .HasForeignKey(x => x.ProduceListingId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.ChangedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

