using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class KycReviewConfiguration : IEntityTypeConfiguration<KycReview>
{
    public void Configure(EntityTypeBuilder<KycReview> builder)
    {
        builder.ToTable("kyc_reviews");
        builder.ConfigureGuidKey(x => x.KycReviewId);
        builder.ConfigureRequiredText(x => x.ReviewAction);
        builder.ConfigureOptionalText(x => x.Remarks);
        builder.ConfigureTimestamp(x => x.ReviewedAt).HasDefaultValueSql("now()");
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasOne<KycApplication>()
            .WithMany()
            .HasForeignKey(x => x.KycApplicationId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.ReviewedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

