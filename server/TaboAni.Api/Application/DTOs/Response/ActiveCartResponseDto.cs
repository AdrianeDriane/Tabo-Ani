namespace TaboAni.Api.Application.DTOs.Response;

public sealed record ActiveCartResponseDto(
    Guid CartId,
    Guid UserId,
    string CartStatus,
    int ItemCount,
    decimal TotalQuantityKg,
    IReadOnlyList<CartItemResponseDto> Items);
