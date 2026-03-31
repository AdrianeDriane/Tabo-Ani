using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class QaReportImageConfiguration : IEntityTypeConfiguration<QaReportImage>
{
    public void Configure(EntityTypeBuilder<QaReportImage> builder)
    {
        builder.ToTable("qa_report_images");
        builder.ConfigureGuidKey(x => x.QaReportImageId);
        builder.ConfigureRequiredText(x => x.ImageUrl);
        builder.Property(x => x.DisplayOrder).HasDefaultValue(1);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.QaReportId, x.DisplayOrder }).IsUnique();
        builder.HasOne<QaReport>()
            .WithMany()
            .HasForeignKey(x => x.QaReportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
