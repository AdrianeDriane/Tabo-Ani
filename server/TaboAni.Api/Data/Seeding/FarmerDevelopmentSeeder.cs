using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Validation;

namespace TaboAni.Api.Data.Seeding;

internal static class FarmerDevelopmentSeeder
{
    private static readonly FarmerSeedDefinition PrimaryFarmerSeed = new(
        UserId: SeedConstants.PrimaryFarmerUserId,
        FarmerProfileId: SeedConstants.PrimaryFarmerProfileId,
        Email: "farmer.one@taboani.local",
        FirstName: "Mateo",
        LastName: "Dela Cruz",
        DisplayName: "Farmer Mateo",
        FarmName: "Green Valley Farm",
        Bio: "Mixed vegetable farm for marketplace testing.",
        FarmLocationText: "Baguio City, Benguet",
        FarmLatitude: 16.402300m,
        FarmLongitude: 120.596000m,
        YearsOfExperience: 8,
        IsPublic: true);

    private static readonly FarmerSeedDefinition SecondaryFarmerSeed = new(
        UserId: SeedConstants.SecondaryFarmerUserId,
        FarmerProfileId: SeedConstants.SecondaryFarmerProfileId,
        Email: "farmer.two@taboani.local",
        FirstName: "Lia",
        LastName: "Santos",
        DisplayName: "Farmer Lia",
        FarmName: "Sunrise Orchard",
        Bio: "Fruit grower profile for ownership validation checks.",
        FarmLocationText: "La Trinidad, Benguet",
        FarmLatitude: 16.455100m,
        FarmLongitude: 120.587500m,
        YearsOfExperience: 5,
        IsPublic: true);

    private static readonly ProduceCategorySeedDefinition VegetablesCategorySeed = new(
        SeedConstants.VegetablesCategoryId,
        "Vegetables",
        "Leafy greens and fresh vegetables.");

    private static readonly ProduceCategorySeedDefinition FruitsCategorySeed = new(
        SeedConstants.FruitsCategoryId,
        "Fruits",
        "Seasonal orchard produce.");

    private static readonly ProduceCategorySeedDefinition GrainsCategorySeed = new(
        SeedConstants.GrainsCategoryId,
        "Grains",
        "Rice, corn, and grain crops.");

    public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
        await context.Database.MigrateAsync(cancellationToken);

        var now = DateTimeOffset.UtcNow;

        // Natural-key lookups keep the seed rerunnable even with partially populated local data.
        var primaryFarmer = await UpsertFarmerAsync(context, PrimaryFarmerSeed, now, cancellationToken);
        var secondaryFarmer = await UpsertFarmerAsync(context, SecondaryFarmerSeed, now, cancellationToken);

        var vegetablesCategory = await UpsertProduceCategoryAsync(context, VegetablesCategorySeed, now, cancellationToken);
        var fruitsCategory = await UpsertProduceCategoryAsync(context, FruitsCategorySeed, now, cancellationToken);
        var grainsCategory = await UpsertProduceCategoryAsync(context, GrainsCategorySeed, now, cancellationToken);

        var primaryListing = await UpsertListingAsync(
            context,
            new ProduceListingSeedDefinition(
                SeedConstants.PrimarySampleListingId,
                primaryFarmer.Profile.FarmerProfileId,
                vegetablesCategory.ProduceCategoryId,
                "Fresh Highland Lettuce",
                "Lettuce",
                "Crisp lettuce harvested from the uplands.",
                95.00m,
                5.000m,
                80.000m,
                ListingStatusPolicy.Active,
                "Baguio City, Benguet",
                16.402300m,
                120.596000m,
                false),
            now,
            cancellationToken);

        var secondaryListing = await UpsertListingAsync(
            context,
            new ProduceListingSeedDefinition(
                SeedConstants.SecondarySampleListingId,
                secondaryFarmer.Profile.FarmerProfileId,
                fruitsCategory.ProduceCategoryId,
                "Sweet Pineapple Harvest",
                "Pineapple",
                "Fresh-picked pineapples for admin and status testing.",
                120.00m,
                10.000m,
                150.000m,
                ListingStatusPolicy.Inactive,
                "La Trinidad, Benguet",
                16.455100m,
                120.587500m,
                false),
            now,
            cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        PrintSeedSummary(
            primaryFarmer,
            secondaryFarmer,
            vegetablesCategory,
            fruitsCategory,
            grainsCategory,
            primaryListing,
            secondaryListing);
    }

    private static async Task<SeededFarmer> UpsertFarmerAsync(
        AppDbContext context,
        FarmerSeedDefinition seed,
        DateTimeOffset now,
        CancellationToken cancellationToken)
    {
        var user = await context.Users
            .SingleOrDefaultAsync(
                existingUser => existingUser.UserId == seed.UserId || existingUser.Email == seed.Email,
                cancellationToken);

        if (user is null)
        {
            user = new User
            {
                UserId = seed.UserId,
                CreatedAt = now
            };

            context.Users.Add(user);
        }

        ApplyUserSeed(user, seed, now);

        var farmerProfile = await context.FarmerProfiles
            .SingleOrDefaultAsync(
                existingProfile =>
                    existingProfile.FarmerProfileId == seed.FarmerProfileId ||
                    existingProfile.UserId == user.UserId,
                cancellationToken);

        if (farmerProfile is null)
        {
            farmerProfile = new FarmerProfile
            {
                FarmerProfileId = seed.FarmerProfileId,
                UserId = user.UserId,
                CreatedAt = now
            };

            context.FarmerProfiles.Add(farmerProfile);
        }

        ApplyFarmerProfileSeed(farmerProfile, seed, user.UserId, now);

        return new SeededFarmer(user, farmerProfile);
    }

    private static void ApplyUserSeed(User user, FarmerSeedDefinition seed, DateTimeOffset now)
    {
        user.Email = seed.Email;
        user.FirstName = seed.FirstName;
        user.LastName = seed.LastName;
        user.DisplayName = seed.DisplayName;
        user.AccountStatus = "ACTIVE";
        user.IsEmailVerified = true;
        user.UpdatedAt = now;
    }

    private static void ApplyFarmerProfileSeed(
        FarmerProfile farmerProfile,
        FarmerSeedDefinition seed,
        Guid userId,
        DateTimeOffset now)
    {
        farmerProfile.UserId = userId;
        farmerProfile.FarmName = seed.FarmName;
        farmerProfile.Bio = seed.Bio;
        farmerProfile.FarmLocationText = seed.FarmLocationText;
        farmerProfile.FarmLatitude = seed.FarmLatitude;
        farmerProfile.FarmLongitude = seed.FarmLongitude;
        farmerProfile.YearsOfExperience = seed.YearsOfExperience;
        farmerProfile.IsPublic = seed.IsPublic;
        farmerProfile.UpdatedAt = now;
    }

    private static async Task<ProduceCategory> UpsertProduceCategoryAsync(
        AppDbContext context,
        ProduceCategorySeedDefinition seed,
        DateTimeOffset now,
        CancellationToken cancellationToken)
    {
        var category = await context.ProduceCategories
            .SingleOrDefaultAsync(
                existingCategory =>
                    existingCategory.ProduceCategoryId == seed.ProduceCategoryId ||
                    existingCategory.CategoryName == seed.CategoryName,
                cancellationToken);

        if (category is null)
        {
            category = new ProduceCategory
            {
                ProduceCategoryId = seed.ProduceCategoryId,
                CreatedAt = now
            };

            context.ProduceCategories.Add(category);
        }

        category.CategoryName = seed.CategoryName;
        category.Description = seed.Description;

        return category;
    }

    private static async Task<ProduceListing> UpsertListingAsync(
        AppDbContext context,
        ProduceListingSeedDefinition seed,
        DateTimeOffset now,
        CancellationToken cancellationToken)
    {
        var listing = await context.ProduceListings
            .SingleOrDefaultAsync(
                existingListing =>
                    existingListing.ProduceListingId == seed.ProduceListingId ||
                    (existingListing.FarmerProfileId == seed.FarmerProfileId &&
                     existingListing.ListingTitle == seed.ListingTitle),
                cancellationToken);

        if (listing is null)
        {
            listing = new ProduceListing
            {
                ProduceListingId = seed.ProduceListingId,
                CreatedAt = now
            };

            context.ProduceListings.Add(listing);
        }

        ApplyListingSeed(listing, seed, now);

        return listing;
    }

    private static void ApplyListingSeed(ProduceListing listing, ProduceListingSeedDefinition seed, DateTimeOffset now)
    {
        listing.FarmerProfileId = seed.FarmerProfileId;
        listing.ProduceCategoryId = seed.ProduceCategoryId;
        listing.ListingTitle = seed.ListingTitle;
        listing.ProduceName = seed.ProduceName;
        listing.Description = seed.Description;
        listing.PricePerKg = seed.PricePerKg;
        listing.MinimumOrderKg = seed.MinimumOrderKg;
        listing.MaximumOrderKg = seed.MaximumOrderKg;
        listing.ListingStatus = ListingStatusPolicy.Normalize(seed.ListingStatus);
        listing.PrimaryLocationText = seed.PrimaryLocationText;
        listing.PrimaryLatitude = seed.PrimaryLatitude;
        listing.PrimaryLongitude = seed.PrimaryLongitude;
        listing.IsPremiumBoosted = seed.IsPremiumBoosted;
        listing.UpdatedAt = now;
    }

    private static void PrintSeedSummary(
        SeededFarmer primaryFarmer,
        SeededFarmer secondaryFarmer,
        ProduceCategory vegetablesCategory,
        ProduceCategory fruitsCategory,
        ProduceCategory grainsCategory,
        ProduceListing primaryListing,
        ProduceListing secondaryListing)
    {
        Console.WriteLine("Farmer development seed completed.");
        Console.WriteLine("Command: dotnet run --project .\\TaboAni.Api\\TaboAni.Api.csproj -- --seed-farmer-data");
        Console.WriteLine();
        Console.WriteLine("Farmers:");
        Console.WriteLine(
            $"  Mateo / {primaryFarmer.User.Email} / UserId: {primaryFarmer.User.UserId} / FarmerProfileId: {primaryFarmer.Profile.FarmerProfileId}");
        Console.WriteLine(
            $"  Lia / {secondaryFarmer.User.Email} / UserId: {secondaryFarmer.User.UserId} / FarmerProfileId: {secondaryFarmer.Profile.FarmerProfileId}");
        Console.WriteLine();
        Console.WriteLine("Produce Categories:");
        Console.WriteLine($"  Vegetables: {vegetablesCategory.ProduceCategoryId}");
        Console.WriteLine($"  Fruits: {fruitsCategory.ProduceCategoryId}");
        Console.WriteLine($"  Grains: {grainsCategory.ProduceCategoryId}");
        Console.WriteLine();
        Console.WriteLine("Sample Listings:");
        Console.WriteLine($"  Fresh Highland Lettuce: {primaryListing.ProduceListingId} ({primaryListing.ListingStatus})");
        Console.WriteLine($"  Sweet Pineapple Harvest: {secondaryListing.ProduceListingId} ({secondaryListing.ListingStatus})");

        // TODO: Move these seed payloads to configuration if shared staging/demo environments need different fixtures.
    }

    private sealed record FarmerSeedDefinition(
        Guid UserId,
        Guid FarmerProfileId,
        string Email,
        string FirstName,
        string LastName,
        string DisplayName,
        string FarmName,
        string? Bio,
        string FarmLocationText,
        decimal? FarmLatitude,
        decimal? FarmLongitude,
        int? YearsOfExperience,
        bool IsPublic);

    private sealed record ProduceCategorySeedDefinition(
        Guid ProduceCategoryId,
        string CategoryName,
        string Description);

    private sealed record ProduceListingSeedDefinition(
        Guid ProduceListingId,
        Guid FarmerProfileId,
        Guid ProduceCategoryId,
        string ListingTitle,
        string ProduceName,
        string? Description,
        decimal PricePerKg,
        decimal MinimumOrderKg,
        decimal? MaximumOrderKg,
        string ListingStatus,
        string PrimaryLocationText,
        decimal? PrimaryLatitude,
        decimal? PrimaryLongitude,
        bool IsPremiumBoosted);

    private sealed record SeededFarmer(User User, FarmerProfile Profile);
}
