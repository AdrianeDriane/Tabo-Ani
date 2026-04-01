namespace TaboAni.Api.Application.DTOs.Response;

public sealed record FarmerProduceListingDetailQueryResultDto(
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

public sealed record FarmerProduceListingListItemQueryResultDto(
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

public sealed record PagedFarmerProduceListingQueryResultDto(
    IReadOnlyList<FarmerProduceListingListItemQueryResultDto> Items,
    int Page,
    int PageSize,
    int TotalCount);
