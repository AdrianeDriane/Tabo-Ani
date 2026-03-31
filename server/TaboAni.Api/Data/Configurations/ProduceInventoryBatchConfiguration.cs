using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class ProduceInventoryBatchConfiguration : IEntityTypeConfiguration<ProduceInventoryBatch>
{
    public void Configure(EntityTypeBuilder<ProduceInventoryBatch> builder)
    {
        builder.ToTable("produce_inventory_batches");
        builder.ConfigureGuidKey(x => x.ProduceInventoryBatchId);
        builder.ConfigureOptionalVarchar(x => x.BatchCode, 100);
        builder.ConfigureOptionalDate(x => x.EstimatedHarvestDate);
        builder.ConfigureOptionalDate(x => x.ActualHarvestDate);
        builder.ConfigureDecimal(x => x.AvailableQuantityKg, 12, 3);
        builder.ConfigureDecimal(x => x.ReservedQuantityKg, 12, 3).HasDefaultValue(0.000m);
        builder.ConfigureRequiredText(x => x.InventoryStatus);
        builder.ConfigureOptionalText(x => x.Notes);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasIndex(x => new { x.ProduceListingId, x.BatchCode })
            .IsUnique()
            .HasDatabaseName("uq_produce_inventory_batches_listing_batch_code")
            .HasFilter("\"batch_code\" IS NOT NULL");
        builder.HasOne<ProduceListing>()
            .WithMany()
            .HasForeignKey(x => x.ProduceListingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
