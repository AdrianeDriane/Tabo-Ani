using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class DistributorProfileConfiguration : IEntityTypeConfiguration<DistributorProfile>
{
    public void Configure(EntityTypeBuilder<DistributorProfile> builder)
    {
        builder.ToTable("distributor_profiles");
        builder.ConfigureGuidKey(x => x.DistributorProfileId);
        builder.ConfigureRequiredVarchar(x => x.FleetDisplayName, 150);
        builder.ConfigureOptionalVarchar(x => x.LicenseNumber, 100);
        builder.ConfigureRequiredText(x => x.BaseLocationText);
        builder.ConfigureOptionalDecimal(x => x.BaseLatitude, 9, 6);
        builder.ConfigureOptionalDecimal(x => x.BaseLongitude, 9, 6);
        builder.Property(x => x.IsAvailable).HasDefaultValue(true);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasIndex(x => x.LicenseNumber)
            .IsUnique()
            .HasDatabaseName("uq_distributor_profiles_license_number")
            .HasFilter("\"license_number\" IS NOT NULL");
        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<DistributorProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

