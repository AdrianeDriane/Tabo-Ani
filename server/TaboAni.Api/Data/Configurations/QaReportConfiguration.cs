using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class QaReportConfiguration : IEntityTypeConfiguration<QaReport>
{
    public void Configure(EntityTypeBuilder<QaReport> builder)
    {
        builder.ToTable("qa_reports", table =>
        {
            table.HasCheckConstraint(
                "ck_qa_reports_fresh_and_damaged_percent",
                "\"fresh_percent\" + \"damaged_percent\" <= 100.00");
        });

        builder.ConfigureGuidKey(x => x.QaReportId);
        builder.ConfigureRequiredText(x => x.QaStage);
        builder.ConfigureDecimal(x => x.FreshPercent, 5, 2);
        builder.ConfigureDecimal(x => x.DamagedPercent, 5, 2);
        builder.ConfigureDecimal(x => x.ExpectedQuantityKg, 12, 3);
        builder.ConfigureDecimal(x => x.ActualQuantityKg, 12, 3);
        builder.ConfigureRequiredText(x => x.OverallCondition);
        builder.ConfigureOptionalText(x => x.Notes);
        builder.ConfigureTimestamp(x => x.SubmittedAt).HasDefaultValueSql("now()");
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasOne<Delivery>()
            .WithMany()
            .HasForeignKey(x => x.DeliveryId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.SubmittedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

