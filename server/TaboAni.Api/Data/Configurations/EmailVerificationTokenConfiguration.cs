using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class EmailVerificationTokenConfiguration : IEntityTypeConfiguration<EmailVerificationToken>
{
    public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
    {
        builder.ToTable("email_verification_tokens");
        builder.ConfigureGuidKey(x => x.EmailVerificationTokenId);
        builder.ConfigureRequiredText(x => x.TokenHash);
        builder.ConfigureTimestamp(x => x.ExpiresAt);
        builder.ConfigureOptionalTimestamp(x => x.ConsumedAt);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.UserId, x.TokenHash })
            .HasDatabaseName("ix_email_verification_tokens_user_id_token_hash");
        builder.HasIndex(x => new { x.UserId, x.ExpiresAt })
            .HasDatabaseName("ix_email_verification_tokens_user_id_expires_at");
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
