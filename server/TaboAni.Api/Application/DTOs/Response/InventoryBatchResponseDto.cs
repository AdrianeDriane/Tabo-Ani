namespace TaboAni.Api.Application.DTOs.Response;

public sealed record InventoryBatchResponseDto(
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
