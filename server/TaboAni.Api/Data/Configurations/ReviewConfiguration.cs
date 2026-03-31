using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews", table =>
        {
            table.HasCheckConstraint("ck_reviews_star_rating", "\"star_rating\" BETWEEN 1 AND 5");
        });

        builder.ConfigureGuidKey(x => x.ReviewId);
        builder.Property(x => x.StarRating).IsRequired();
        builder.ConfigureOptionalText(x => x.ReviewText);
        builder.ConfigureRequiredText(x => x.ReviewStatus).HasDefaultValue("PUBLISHED");
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasIndex(x => new { x.ProduceListingId, x.ReviewStatus, x.CreatedAt })
            .HasDatabaseName("ix_reviews_listing_status_created_at");
        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<ProduceListing>()
            .WithMany()
            .HasForeignKey(x => x.ProduceListingId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.ReviewerUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

