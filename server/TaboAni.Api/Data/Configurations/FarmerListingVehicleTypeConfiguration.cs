using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class FarmerListingVehicleTypeConfiguration : IEntityTypeConfiguration<FarmerListingVehicleType>
{
    public void Configure(EntityTypeBuilder<FarmerListingVehicleType> builder)
    {
        builder.ToTable("farmer_listing_vehicle_types");
        builder.HasKey(x => new { x.ProduceListingId, x.VehicleTypeId });
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasOne(x => x.ProduceListing)
            .WithMany(listing => listing.AllowedVehicleTypes)
            .HasForeignKey(x => x.ProduceListingId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.VehicleType)
            .WithMany(vehicleType => vehicleType.FarmerListings)
            .HasForeignKey(x => x.VehicleTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

