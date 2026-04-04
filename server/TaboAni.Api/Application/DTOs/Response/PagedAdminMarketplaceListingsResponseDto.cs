namespace TaboAni.Api.Application.DTOs.Response;

public sealed record PagedAdminMarketplaceListingsResponseDto(
    IReadOnlyList<AdminMarketplaceListingResponseDto> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages);
