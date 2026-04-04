namespace TaboAni.Api.Application.DTOs.Request;

public sealed record UpdateInventoryBatchRequestDto(
    string? BatchCode,
    DateOnly? EstimatedHarvestDate,
    DateOnly? ActualHarvestDate,
    decimal AvailableQuantityKg,
    decimal ReservedQuantityKg,
    string? Notes);
