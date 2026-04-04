using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Application.DTOs.Response;

public sealed record CartItemQueryResultDto(
    Guid CartItemId,
    Guid ProduceListingId,
    Guid FarmerProfileId,
    string FarmerFarmName,
    string ListingTitle,
    string ProduceName,
    decimal PricePerKg,
    decimal QuantityKg,
    decimal MinimumOrderKg,
    decimal? MaximumOrderKg,
    string PrimaryLocationText,
    string? PrimaryImageUrl);

public sealed record ActiveCartQueryResultDto(
    Guid CartId,
    Guid UserId,
    CartStatus CartStatus,
    IReadOnlyList<CartItemQueryResultDto> Items);
