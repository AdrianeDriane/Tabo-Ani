namespace TaboAni.Api.Application.DTOs.Response;

public sealed record PagedFarmerProduceListingsResponseDto(
    IReadOnlyList<FarmerProduceListingListItemResponseDto> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages);
