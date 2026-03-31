using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class ListingAvailabilityWindowConfiguration : IEntityTypeConfiguration<ListingAvailabilityWindow>
{
    public void Configure(EntityTypeBuilder<ListingAvailabilityWindow> builder)
    {
        builder.ToTable("listing_availability_windows", table =>
        {
            table.HasCheckConstraint(
                "ck_listing_availability_windows_date_range",
                "\"available_to_date\" >= \"available_from_date\"");
        });

        builder.ConfigureGuidKey(x => x.ListingAvailabilityWindowId);
        builder.ConfigureDate(x => x.AvailableFromDate);
        builder.ConfigureDate(x => x.AvailableToDate);
        builder.ConfigureOptionalText(x => x.Notes);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasOne<ProduceListing>()
            .WithMany()
            .HasForeignKey(x => x.ProduceListingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
