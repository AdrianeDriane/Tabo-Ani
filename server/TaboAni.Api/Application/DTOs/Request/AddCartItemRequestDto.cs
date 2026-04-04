namespace TaboAni.Api.Application.DTOs.Request;

public sealed record AddCartItemRequestDto(
    Guid ProduceListingId,
    decimal QuantityKg);
