namespace TaboAni.Api.Application.DTOs.Response;

public sealed record ListingPriceUpdateResultDto(
    FarmerProduceListingDetailResponseDto Listing,
    bool PriceChanged);
