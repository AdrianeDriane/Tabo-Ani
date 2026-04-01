namespace TaboAni.Api.Application.DTOs.Response;

public sealed record InventoryBatchQueryResultDto(
    Guid ProduceInventoryBatchId,
    Guid ProduceListingId,
    string? BatchCode,
    DateOnly? EstimatedHarvestDate,
    DateOnly? ActualHarvestDate,
    decimal AvailableQuantityKg,
    decimal ReservedQuantityKg,
    string InventoryStatus,
    string? Notes,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);

public sealed record FarmerListingInventoryQueryResultDto(
    Guid ProduceListingId,
    string ListingTitle,
    string ProduceName,
    IReadOnlyList<InventoryBatchQueryResultDto> Batches);
