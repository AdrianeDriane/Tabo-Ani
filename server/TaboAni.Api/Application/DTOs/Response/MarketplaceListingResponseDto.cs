namespace TaboAni.Api.Application.DTOs.Response;

public sealed record MarketplaceListingResponseDto(
    Guid ProduceListingId,
    Guid ProduceCategoryId,
    string CategoryName,
    Guid FarmerProfileId,
    string FarmerFarmName,
    string ListingTitle,
    string ProduceName,
    string? Description,
    decimal PricePerKg,
    decimal MinimumOrderKg,
    decimal? MaximumOrderKg,
    string PrimaryLocationText,
    bool IsPremiumBoosted,
    string? PrimaryImageUrl,
    DateTimeOffset CreatedAt);
