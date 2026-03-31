using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class ConversationParticipantConfiguration : IEntityTypeConfiguration<ConversationParticipant>
{
    public void Configure(EntityTypeBuilder<ConversationParticipant> builder)
    {
        builder.ToTable("conversation_participants");
        builder.ConfigureGuidKey(x => x.ConversationParticipantId);
        builder.ConfigureRequiredVarchar(x => x.ParticipantRoleCode, 50);
        builder.ConfigureTimestamp(x => x.JoinedAt).HasDefaultValueSql("now()");
        builder.ConfigureOptionalTimestamp(x => x.LeftAt);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.ConversationId, x.UserId }).IsUnique();
        builder.HasOne<Conversation>()
            .WithMany()
            .HasForeignKey(x => x.ConversationId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
