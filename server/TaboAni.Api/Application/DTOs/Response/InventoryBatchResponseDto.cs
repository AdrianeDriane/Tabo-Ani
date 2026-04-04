using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Application.DTOs.Response;

public sealed record InventoryBatchResponseDto(
    Guid ProduceInventoryBatchId,
    Guid ProduceListingId,
    string? BatchCode,
    DateOnly? EstimatedHarvestDate,
    DateOnly? ActualHarvestDate,
    decimal AvailableQuantityKg,
    decimal ReservedQuantityKg,
    InventoryStatus InventoryStatus,
    string? Notes,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);
