namespace TaboAni.Api.Application.DTOs.Request;

public sealed record FarmerOwnListingsQueryRequestDto(
    string? Q,
    string? ListingStatus,
    int Page = 1,
    int PageSize = 20,
    string? Sort = null);
