using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class KycApplicationConfiguration : IEntityTypeConfiguration<KycApplication>
{
    public void Configure(EntityTypeBuilder<KycApplication> builder)
    {
        builder.ToTable("kyc_applications");
        builder.ConfigureGuidKey(x => x.KycApplicationId);
        builder.ConfigureRequiredText(x => x.ApplicationStatus);
        builder.ConfigureTimestamp(x => x.SubmittedAt).HasDefaultValueSql("now()");
        builder.ConfigureOptionalTimestamp(x => x.ReviewedAt);
        builder.ConfigureOptionalText(x => x.FinalRemarks);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasIndex(x => new { x.ApplicationStatus, x.SubmittedAt })
            .HasDatabaseName("ix_kyc_applications_status_submitted_at");
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

