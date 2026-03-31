using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class DeliveryAssignmentConfiguration : IEntityTypeConfiguration<DeliveryAssignment>
{
    public void Configure(EntityTypeBuilder<DeliveryAssignment> builder)
    {
        builder.ToTable("delivery_assignments");
        builder.ConfigureGuidKey(x => x.DeliveryAssignmentId);
        builder.ConfigureRequiredText(x => x.AssignmentStatus);
        builder.ConfigureTimestamp(x => x.AssignedAt).HasDefaultValueSql("now()");
        builder.ConfigureOptionalTimestamp(x => x.EndedAt);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasOne<Delivery>()
            .WithMany()
            .HasForeignKey(x => x.DeliveryId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.DistributorUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

