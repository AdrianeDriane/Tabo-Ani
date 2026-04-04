namespace TaboAni.Api.Application.DTOs.Response;

public sealed record PagedMarketplaceListingsResponseDto(
    IReadOnlyList<MarketplaceListingResponseDto> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages);
