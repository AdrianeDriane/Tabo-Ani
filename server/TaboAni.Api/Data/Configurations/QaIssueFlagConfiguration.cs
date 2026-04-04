using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class QaIssueFlagConfiguration : IEntityTypeConfiguration<QaIssueFlag>
{
    public void Configure(EntityTypeBuilder<QaIssueFlag> builder)
    {
        builder.ToTable("qa_issue_flags");
        builder.ConfigureGuidKey(x => x.QaIssueFlagId);
        builder.ConfigureRequiredVarchar(x => x.IssueType, 100);
        builder.ConfigureRequiredText(x => x.Severity);
        builder.ConfigureRequiredText(x => x.Description);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasOne<QaReport>()
            .WithMany()
            .HasForeignKey(x => x.QaReportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

