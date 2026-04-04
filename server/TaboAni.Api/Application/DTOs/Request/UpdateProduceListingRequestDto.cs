namespace TaboAni.Api.Application.DTOs.Request;

public sealed record UpdateProduceListingRequestDto(
    Guid ProduceCategoryId,
    string ListingTitle,
    string ProduceName,
    string? Description,
    decimal PricePerKg,
    decimal MinimumOrderKg,
    decimal? MaximumOrderKg,
    string PrimaryLocationText,
    decimal? PrimaryLatitude,
    decimal? PrimaryLongitude);
