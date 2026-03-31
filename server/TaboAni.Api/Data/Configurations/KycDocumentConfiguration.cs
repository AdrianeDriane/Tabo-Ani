using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class KycDocumentConfiguration : IEntityTypeConfiguration<KycDocument>
{
    public void Configure(EntityTypeBuilder<KycDocument> builder)
    {
        builder.ToTable("kyc_documents");
        builder.ConfigureGuidKey(x => x.KycDocumentId);
        builder.ConfigureRequiredVarchar(x => x.DocumentType, 100);
        builder.ConfigureRequiredText(x => x.FileUrl);
        builder.ConfigureRequiredVarchar(x => x.FileName, 255);
        builder.ConfigureRequiredVarchar(x => x.MimeType, 100);
        builder.ConfigureRequiredText(x => x.DocumentStatus).HasDefaultValue("PENDING");
        builder.ConfigureOptionalText(x => x.RejectionReason);
        builder.ConfigureTimestamp(x => x.UploadedAt).HasDefaultValueSql("now()");
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasOne<KycApplication>()
            .WithMany()
            .HasForeignKey(x => x.KycApplicationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
