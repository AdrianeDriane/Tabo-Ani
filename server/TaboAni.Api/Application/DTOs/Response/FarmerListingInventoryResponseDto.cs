namespace TaboAni.Api.Application.DTOs.Response;

public sealed record FarmerListingInventoryResponseDto(
    Guid ProduceListingId,
    string ListingTitle,
    string ProduceName,
    decimal TotalAvailableQuantityKg,
    decimal TotalReservedQuantityKg,
    IReadOnlyList<InventoryBatchResponseDto> Batches);
