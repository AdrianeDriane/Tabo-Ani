using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");
        builder.ConfigureGuidKey(x => x.MessageId);
        builder.ConfigureRequiredText(x => x.MessageBody);
        builder.ConfigureRequiredText(x => x.MessageType).HasDefaultValue("TEXT");
        builder.ConfigureTimestamp(x => x.SentAt).HasDefaultValueSql("now()");
        builder.ConfigureOptionalTimestamp(x => x.EditedAt);
        builder.ConfigureOptionalTimestamp(x => x.DeletedAt);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.ConversationId, x.SentAt })
            .HasDatabaseName("ix_messages_conversation_sent_at");
        builder.HasOne<Conversation>()
            .WithMany()
            .HasForeignKey(x => x.ConversationId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.SenderUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
