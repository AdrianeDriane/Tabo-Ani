using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Data.Seeders;

public static class OrderTestDataSeeder
{
    private static readonly Guid BuyerUserId = Guid.Parse("7b48c50b-5a79-4c55-89e4-1d1adcc8f101");
    private static readonly Guid FarmerUserId = Guid.Parse("f2024b3f-0ec5-4bf0-a1aa-63d666a37d01");
    private static readonly Guid FarmerProfileId = Guid.Parse("d8c91ac5-d1c9-4d10-97ac-94e4dc781201");
    private static readonly Guid ProduceCategoryId = Guid.Parse("57b88d0b-36d4-4f0d-977a-865a7df9e301");
    private static readonly Guid ProduceListingId = Guid.Parse("bb596370-3ad1-4936-8421-0ef2ab889401");

    public static async Task SeedAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        await using var scope = services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync(cancellationToken);

        var now = DateTimeOffset.UtcNow;

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await UpsertEntityAsync(
                context,
                context.Users,
                user => user.UserId == BuyerUserId,
                BuildBuyer(now),
                cancellationToken);

            await UpsertEntityAsync(
                context,
                context.Users,
                user => user.UserId == FarmerUserId,
                BuildFarmerUser(now),
                cancellationToken);

            await UpsertEntityAsync(
                context,
                context.FarmerProfiles,
                profile => profile.FarmerProfileId == FarmerProfileId,
                BuildFarmerProfile(now),
                cancellationToken);

            await UpsertEntityAsync(
                context,
                context.ProduceCategories,
                category => category.ProduceCategoryId == ProduceCategoryId,
                BuildProduceCategory(now),
                cancellationToken);

            await UpsertEntityAsync(
                context,
                context.ProduceListings,
                listing => listing.ProduceListingId == ProduceListingId,
                BuildProduceListing(now),
                cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            WriteSeedSummary();
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private static User BuildBuyer(DateTimeOffset now)
    {
        return new User
        {
            UserId = BuyerUserId,
            Email = "seed.buyer.orders@taboani.local",
            FirstName = "Seeded",
            LastName = "Buyer",
            DisplayName = "Seed Buyer",
            IsEmailVerified = true,
            IsMobileVerified = false,
            AccountStatus = "Active",
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    private static User BuildFarmerUser(DateTimeOffset now)
    {
        return new User
        {
            UserId = FarmerUserId,
            Email = "seed.farmer.orders@taboani.local",
            FirstName = "Seeded",
            LastName = "Farmer",
            DisplayName = "Seed Farmer",
            IsEmailVerified = true,
            IsMobileVerified = false,
            AccountStatus = "Active",
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    private static FarmerProfile BuildFarmerProfile(DateTimeOffset now)
    {
        return new FarmerProfile
        {
            FarmerProfileId = FarmerProfileId,
            UserId = FarmerUserId,
            FarmName = "TaboAni Seeder Farm",
            Bio = "Farmer profile seeded for order creation tests.",
            FarmLocationText = "Baguio City, Philippines",
            FarmLatitude = 16.402300m,
            FarmLongitude = 120.596000m,
            YearsOfExperience = 8,
            IsPublic = true,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    private static ProduceCategory BuildProduceCategory(DateTimeOffset now)
    {
        return new ProduceCategory
        {
            ProduceCategoryId = ProduceCategoryId,
            CategoryName = "Seeder Vegetables",
            Description = "Produce category seeded for order creation tests.",
            CreatedAt = now
        };
    }

    private static ProduceListing BuildProduceListing(DateTimeOffset now)
    {
        return new ProduceListing
        {
            ProduceListingId = ProduceListingId,
            FarmerProfileId = FarmerProfileId,
            ProduceCategoryId = ProduceCategoryId,
            ListingTitle = "Seeder Premium Carrots",
            ProduceName = "Carrots",
            Description = "Produce listing seeded for order creation tests.",
            PricePerKg = 150.00m,
            MinimumOrderKg = 1.000m,
            MaximumOrderKg = 10.000m,
            ListingStatus = "Active",
            PrimaryLocationText = "La Trinidad, Benguet",
            PrimaryLatitude = 16.455000m,
            PrimaryLongitude = 120.587000m,
            IsPremiumBoosted = false,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    private static async Task UpsertEntityAsync<TEntity>(
        AppDbContext context,
        DbSet<TEntity> dbSet,
        Expression<Func<TEntity, bool>> predicate,
        TEntity entity,
        CancellationToken cancellationToken)
        where TEntity : class
    {
        var existingEntity = await dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

        if (existingEntity is null)
        {
            await dbSet.AddAsync(entity, cancellationToken);
            return;
        }

        context.Entry(existingEntity).CurrentValues.SetValues(entity);
    }

    private static void WriteSeedSummary()
    {
        Console.WriteLine("Order dependency data seeded successfully.");
        Console.WriteLine($"BuyerUserId: {BuyerUserId}");
        Console.WriteLine($"FarmerUserId: {FarmerUserId}");
        Console.WriteLine($"FarmerProfileId: {FarmerProfileId}");
        Console.WriteLine($"ProduceCategoryId: {ProduceCategoryId}");
        Console.WriteLine($"ProduceListingId: {ProduceListingId}");
        Console.WriteLine("Run with: dotnet run -- --seed-order-test-data");
    }
}
