using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data.Configurations;

internal sealed class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("wallets");
        builder.ConfigureGuidKey(x => x.WalletId);
        builder.ConfigureDecimal(x => x.AvailableBalance, 12, 2).HasDefaultValue(0.00m);
        builder.ConfigureDecimal(x => x.HeldBalance, 12, 2).HasDefaultValue(0.00m);
        builder.ConfigureRequiredText(x => x.WalletStatus).HasDefaultValue("ACTIVE");
        builder.ConfigureCreatedAt(x => x.CreatedAt);
        builder.ConfigureUpdatedAt(x => x.UpdatedAt);
        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Wallet>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
