using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class FarmerListingVehicleTypeConfiguration : IEntityTypeConfiguration<FarmerListingVehicleType>
{
    public void Configure(EntityTypeBuilder<FarmerListingVehicleType> builder)
    {
        builder.ToTable("farmer_listing_vehicle_types");
        builder.ConfigureGuidKey(x => x.FarmerListingVehicleTypeId);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.ProduceListingId, x.VehicleTypeId }).IsUnique();
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
