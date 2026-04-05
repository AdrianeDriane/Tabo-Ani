using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class UserPolicyAcceptanceConfiguration : IEntityTypeConfiguration<UserPolicyAcceptance>
{
    public void Configure(EntityTypeBuilder<UserPolicyAcceptance> builder)
    {
        builder.ToTable("user_policy_acceptances");
        builder.ConfigureGuidKey(x => x.UserPolicyAcceptanceId);
        builder.ConfigureRequiredText(x => x.PolicyType);
        builder.ConfigureRequiredVarchar(x => x.PolicyVersion, 50);
        builder.ConfigureTimestamp(x => x.AcceptedAt);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => new { x.UserId, x.PolicyType, x.AcceptedAt })
            .HasDatabaseName("ix_user_policy_acceptances_user_id_policy_type_accepted_at");
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
