using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;

namespace TaboAni.Api.Application.Interfaces.Service;

public interface ICartService
{
    Task<ActiveCartResponseDto> GetActiveCartAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<ActiveCartResponseDto> AddItemAsync(
        Guid userId,
        AddCartItemRequestDto request,
        CancellationToken cancellationToken = default);

    Task<ActiveCartResponseDto> UpdateItemQuantityAsync(
        Guid userId,
        Guid cartItemId,
        UpdateCartItemQuantityRequestDto request,
        CancellationToken cancellationToken = default);

    Task RemoveItemAsync(Guid userId, Guid cartItemId, CancellationToken cancellationToken = default);
}
