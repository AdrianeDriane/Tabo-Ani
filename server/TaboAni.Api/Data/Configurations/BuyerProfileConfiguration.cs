using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class BuyerProfileConfiguration : IEntityTypeConfiguration<BuyerProfile>
{
    public void Configure(EntityTypeBuilder<BuyerProfile> builder)
    {
        builder.ToTable("buyer_profiles");
        builder.ConfigureGuidKey(x => x.BuyerProfileId);
        builder.ConfigureRequiredVarchar(x => x.BusinessName, 150);
        builder.ConfigureRequiredVarchar(x => x.ContactPersonName, 150);
        builder.ConfigureRequiredVarchar(x => x.BusinessType, 100);
        builder.ConfigureRequiredText(x => x.BusinessLocationText);
        builder.ConfigureOptionalDecimal(x => x.BusinessLatitude, 9, 6);
        builder.ConfigureOptionalDecimal(x => x.BusinessLongitude, 9, 6);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<BuyerProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
