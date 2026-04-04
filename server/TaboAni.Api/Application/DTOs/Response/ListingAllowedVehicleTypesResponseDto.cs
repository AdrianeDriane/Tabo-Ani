namespace TaboAni.Api.Application.DTOs.Response;

public sealed record ListingAllowedVehicleTypeResponseDto(
    Guid VehicleTypeId,
    string VehicleTypeName,
    string? Description,
    decimal MaxCapacityKg,
    bool IsActive);

public sealed record ListingAllowedVehicleTypesResponseDto(
    Guid ProduceListingId,
    IReadOnlyList<ListingAllowedVehicleTypeResponseDto> VehicleTypes);
