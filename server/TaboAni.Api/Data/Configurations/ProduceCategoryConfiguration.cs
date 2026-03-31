using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class ProduceCategoryConfiguration : IEntityTypeConfiguration<ProduceCategory>
{
    public void Configure(EntityTypeBuilder<ProduceCategory> builder)
    {
        builder.ToTable("produce_categories");
        builder.ConfigureGuidKey(x => x.ProduceCategoryId);
        builder.ConfigureRequiredVarchar(x => x.CategoryName, 100);
        builder.ConfigureOptionalText(x => x.Description);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.HasIndex(x => x.CategoryName).IsUnique();
    }
}
