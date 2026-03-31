using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class FarmerPayoutConfiguration : IEntityTypeConfiguration<FarmerPayout>
{
    public void Configure(EntityTypeBuilder<FarmerPayout> builder)
    {
        builder.ToTable("farmer_payouts");
        builder.ConfigureGuidKey(x => x.FarmerPayoutId);
        builder.ConfigureDecimal(x => x.GrossAmount, 12, 2);
        builder.ConfigureDecimal(x => x.PlatformFeeAmount, 12, 2).HasDefaultValue(0.00m);
        builder.ConfigureDecimal(x => x.NetAmount, 12, 2);
        builder.ConfigureRequiredText(x => x.PayoutStatus);
        builder.ConfigureOptionalTimestamp(x => x.ReleasedAt);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasIndex(x => new { x.OrderId, x.FarmerProfileId }).IsUnique();
        builder.HasOne<FarmerProfile>()
            .WithMany()
            .HasForeignKey(x => x.FarmerProfileId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
