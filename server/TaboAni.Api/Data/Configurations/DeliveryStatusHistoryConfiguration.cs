using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class DeliveryStatusHistoryConfiguration : IEntityTypeConfiguration<DeliveryStatusHistory>
{
    public void Configure(EntityTypeBuilder<DeliveryStatusHistory> builder)
    {
        builder.ToTable("delivery_status_history");
        builder.ConfigureGuidKey(x => x.DeliveryStatusHistoryId);
        builder.ConfigureOptionalText(x => x.FromStatus);
        builder.ConfigureRequiredText(x => x.ToStatus);
        builder.ConfigureOptionalText(x => x.Remarks);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.DeliveryId, x.CreatedAt })
            .HasDatabaseName("ix_delivery_status_history_delivery_created_at");
        builder.HasOne<Delivery>()
            .WithMany()
            .HasForeignKey(x => x.DeliveryId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.TriggeredByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
