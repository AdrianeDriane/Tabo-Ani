namespace TaboAni.Api.Application.DTOs.Request;

public sealed record MarketplaceListingsQueryRequestDto(
    string? Q,
    Guid? CategoryId,
    string? Location,
    decimal? MinPrice,
    decimal? MaxPrice,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null,
    string? ListingStatus = null);
