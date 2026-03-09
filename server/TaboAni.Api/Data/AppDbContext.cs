using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Models;

namespace TaboAni.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ProduceListing> ProduceListings => Set<ProduceListing>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProduceListing>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Category)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.PricePerKg)
                .HasColumnType("numeric(10,2)");
        });
    }
}