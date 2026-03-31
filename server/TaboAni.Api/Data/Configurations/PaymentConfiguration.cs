using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payments");
        builder.ConfigureGuidKey(x => x.PaymentId);
        builder.ConfigureRequiredText(x => x.PaymentType);
        builder.ConfigureRequiredText(x => x.PaymentMethod);
        builder.ConfigureRequiredText(x => x.PaymentStatus);
        builder.ConfigureDecimal(x => x.Amount, 12, 2);
        builder.ConfigureOptionalVarchar(x => x.ExternalReference, 150);
        builder.ConfigureOptionalTimestamp(x => x.PaidAt);
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasOne<Order>()
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
