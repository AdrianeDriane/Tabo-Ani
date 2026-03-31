using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class FarmerProfileConfiguration : IEntityTypeConfiguration<FarmerProfile>
{
    public void Configure(EntityTypeBuilder<FarmerProfile> builder)
    {
        builder.ToTable("farmer_profiles");
        builder.ConfigureGuidKey(x => x.FarmerProfileId);
        builder.ConfigureRequiredVarchar(x => x.FarmName, 150);
        builder.ConfigureOptionalText(x => x.Bio);
        builder.ConfigureRequiredText(x => x.FarmLocationText);
        builder.ConfigureOptionalDecimal(x => x.FarmLatitude, 9, 6);
        builder.ConfigureOptionalDecimal(x => x.FarmLongitude, 9, 6);
        builder.Property(x => x.IsPublic).HasDefaultValue(true);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<FarmerProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
