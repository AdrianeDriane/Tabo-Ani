using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("deliveries");
        builder.ConfigureGuidKey(x => x.DeliveryId);
        builder.ConfigureRequiredVarchar(x => x.DeliveryCode, 50);
        builder.ConfigureRequiredText(x => x.DeliveryStatus);
        builder.ConfigureOptionalTimestamp(x => x.PlannedPickupDate);
        builder.ConfigureOptionalTimestamp(x => x.ActualPickupAt);
        builder.ConfigureOptionalTimestamp(x => x.ActualArrivalAt);
        builder.ConfigureDecimal(x => x.TotalReservedCapacityKg, 12, 3).HasDefaultValue(0.000m);
        builder.ConfigureOptionalText(x => x.Notes);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasIndex(x => x.DeliveryCode).IsUnique();
        builder.HasIndex(x => new { x.DeliveryStatus, x.PlannedPickupDate })
            .HasDatabaseName("ix_deliveries_status_planned_pickup");
        builder.HasOne<VehicleType>()
            .WithMany()
            .HasForeignKey(x => x.VehicleTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
