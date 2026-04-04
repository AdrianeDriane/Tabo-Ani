namespace TaboAni.Api.Application.DTOs.Response;

public sealed record CartItemResponseDto(
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
