namespace TaboAni.Api.Application.DTOs.Response;

public sealed record ListingAllowedVehicleTypeQueryResultDto(
    Guid VehicleTypeId,
    string VehicleTypeName,
    string? Description,
    decimal MaxCapacityKg,
    bool IsActive);
