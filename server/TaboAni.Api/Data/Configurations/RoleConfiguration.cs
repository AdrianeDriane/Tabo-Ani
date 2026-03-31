using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.ConfigureGuidKey(x => x.RoleId);
        builder.ConfigureRequiredVarchar(x => x.RoleCode, 50);
        builder.ConfigureRequiredVarchar(x => x.RoleName, 100);
        builder.ConfigureOptionalText(x => x.Description);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => x.RoleCode).IsUnique();
        builder.HasIndex(x => x.RoleName).IsUnique();
    }
}

