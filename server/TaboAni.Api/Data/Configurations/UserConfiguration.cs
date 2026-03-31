using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", table =>
        {
            table.HasCheckConstraint("ck_users_contact", "\"email\" IS NOT NULL OR \"mobile_number\" IS NOT NULL");
        });

        builder.ConfigureGuidKey(x => x.UserId);
        builder.ConfigureOptionalVarchar(x => x.Email, 255);
        builder.ConfigureOptionalVarchar(x => x.MobileNumber, 20);
        builder.ConfigureOptionalText(x => x.PasswordHash);
        builder.ConfigureRequiredVarchar(x => x.FirstName, 100);
        builder.ConfigureRequiredVarchar(x => x.LastName, 100);
        builder.ConfigureOptionalVarchar(x => x.DisplayName, 150);
        builder.ConfigureOptionalText(x => x.ProfilePhotoUrl);
        builder.Property(x => x.IsEmailVerified).HasDefaultValue(false);
        builder.Property(x => x.IsMobileVerified).HasDefaultValue(false);
        builder.ConfigureRequiredText(x => x.AccountStatus);
        builder.ConfigureOptionalTimestamp(x => x.LastLoginAt);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasIndex(x => x.Email)
            .IsUnique()
            .HasDatabaseName("uq_users_email")
            .HasFilter("\"email\" IS NOT NULL");
        builder.HasIndex(x => x.MobileNumber)
            .IsUnique()
            .HasDatabaseName("uq_users_mobile_number")
            .HasFilter("\"mobile_number\" IS NOT NULL");
    }
}
