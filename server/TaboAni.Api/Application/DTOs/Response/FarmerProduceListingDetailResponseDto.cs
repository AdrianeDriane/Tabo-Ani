namespace TaboAni.Api.Application.DTOs.Response;

public sealed record FarmerProduceListingDetailResponseDto(
    Guid ProduceListingId,
    Guid FarmerProfileId,
    string FarmerFarmName,
    Guid ProduceCategoryId,
    string CategoryName,
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
    bool IsPremiumBoosted,
    string? PrimaryImageUrl,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);
