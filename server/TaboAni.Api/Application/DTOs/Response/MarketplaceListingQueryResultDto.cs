namespace TaboAni.Api.Application.DTOs.Response;

public sealed record MarketplaceListingQueryResultItemDto(
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

public sealed record PagedMarketplaceListingQueryResultDto(
    IReadOnlyList<MarketplaceListingQueryResultItemDto> Items,
    int Page,
    int PageSize,
    int TotalCount);
