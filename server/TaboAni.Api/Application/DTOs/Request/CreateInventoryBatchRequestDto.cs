namespace TaboAni.Api.Application.DTOs.Request;

public sealed record CreateInventoryBatchRequestDto(
    string? BatchCode,
    DateOnly? EstimatedHarvestDate,
    DateOnly? ActualHarvestDate,
    decimal AvailableQuantityKg,
    decimal ReservedQuantityKg,
    string? Notes);
