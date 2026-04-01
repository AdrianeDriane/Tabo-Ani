namespace TaboAni.Api.Application.DTOs.Response;

public sealed record CartListingSnapshotDto(
    Guid ProduceListingId,
    Guid FarmerProfileId,
    string FarmerFarmName,
    string ListingTitle,
    string ProduceName,
    decimal PricePerKg,
    decimal MinimumOrderKg,
    decimal? MaximumOrderKg,
    string ListingStatus,
    string PrimaryLocationText,
    string? PrimaryImageUrl);
