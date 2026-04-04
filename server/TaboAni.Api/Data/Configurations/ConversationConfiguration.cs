using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.ToTable("conversations");
        builder.ConfigureGuidKey(x => x.ConversationId);
        builder.ConfigureRequiredText(x => x.ConversationStatus).HasDefaultValue("ACTIVE");
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasOne<Order>()
            .WithOne()
            .HasForeignKey<Conversation>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

