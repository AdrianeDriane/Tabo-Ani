namespace TaboAni.Api.Application.DTOs.Response;

public sealed record AdminMarketplaceListingResponseDto(
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
    string ListingStatus,
    string? PrimaryImageUrl,
    DateTimeOffset CreatedAt);
