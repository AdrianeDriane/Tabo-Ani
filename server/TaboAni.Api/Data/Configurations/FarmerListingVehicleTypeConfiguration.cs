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
        builder.HasOne<ProduceListing>()
            .WithMany()
            .HasForeignKey(x => x.ProduceListingId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<VehicleType>()
            .WithMany()
            .HasForeignKey(x => x.VehicleTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

