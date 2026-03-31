using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("audit_logs");
        builder.ConfigureGuidKey(x => x.AuditLogId);
        builder.ConfigureRequiredVarchar(x => x.EntityType, 100);
        builder.ConfigureRequiredVarchar(x => x.ActionType, 100);
        builder.ConfigureRequiredText(x => x.ActionSummary);
        builder.Property(x => x.Metadata).HasColumnType("jsonb");
        builder.Property(x => x.IpAddress).HasColumnType("inet");
        builder.ConfigureOptionalText(x => x.UserAgent);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.EntityType, x.EntityId, x.CreatedAt })
            .HasDatabaseName("ix_audit_logs_entity_lookup");
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.ActorUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
