using TaboAni.Api.Domain.Enums;

namespace TaboAni.Api.Application.DTOs.Response;

public sealed record ActiveCartResponseDto(
    Guid CartId,
    Guid UserId,
    CartStatus CartStatus,
    int ItemCount,
    decimal TotalQuantityKg,
    IReadOnlyList<CartItemResponseDto> Items);
