using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");
        builder.ConfigureGuidKey(x => x.RefreshTokenId);
        builder.ConfigureRequiredText(x => x.TokenHash);
        builder.Property(x => x.IsPersistent).HasDefaultValue(false);
        builder.ConfigureTimestamp(x => x.ExpiresAt);
        builder.ConfigureOptionalTimestamp(x => x.InvalidatedAt);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => x.TokenHash)
            .IsUnique()
            .HasDatabaseName("ux_refresh_tokens_token_hash");
        builder.HasIndex(x => new { x.UserId, x.ExpiresAt })
            .HasDatabaseName("ix_refresh_tokens_user_id_expires_at");
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
