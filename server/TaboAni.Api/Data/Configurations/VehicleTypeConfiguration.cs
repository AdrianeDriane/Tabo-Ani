using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
{
    public void Configure(EntityTypeBuilder<VehicleType> builder)
    {
        builder.ToTable("vehicle_types");
        builder.ConfigureGuidKey(x => x.VehicleTypeId);
        builder.ConfigureRequiredVarchar(x => x.VehicleTypeName, 100);
        builder.ConfigureOptionalText(x => x.Description);
        builder.ConfigureDecimal(x => x.MaxCapacityKg, 12, 3);
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => x.VehicleTypeName).IsUnique();
    }
}

