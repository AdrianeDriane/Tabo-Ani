namespace TaboAni.Api.Application.DTOs.Response;

public sealed record FarmerProduceListingListItemResponseDto(
    Guid ProduceListingId,
    Guid ProduceCategoryId,
    string CategoryName,
    string ListingTitle,
    string ProduceName,
    decimal PricePerKg,
    string ListingStatus,
    string PrimaryLocationText,
    bool IsPremiumBoosted,
    string? PrimaryImageUrl,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);
