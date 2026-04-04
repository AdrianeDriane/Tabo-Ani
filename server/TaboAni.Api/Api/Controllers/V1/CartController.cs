using Microsoft.AspNetCore.Mvc;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Service;

namespace TaboAni.Api.Controllers.V1;

[ApiController]
[Route("api/v1/users/{userId:guid}/cart")]
public sealed class CartController(ICartService cartService) : ControllerBase
{
    private readonly ICartService _cartService = cartService;

    // TODO: Replace route-scoped user ownership with authenticated user context once auth is introduced.
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseDto<ActiveCartResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetActiveCart(Guid userId, CancellationToken cancellationToken)
    {
        var cart = await _cartService.GetActiveCartAsync(userId, cancellationToken);

        return Ok(new ApiResponseDto<ActiveCartResponseDto>
        {
            Success = true,
            Message = "Active cart retrieved successfully.",
            Data = cart
        });
    }

    [HttpPost("items")]
    [ProducesResponseType(typeof(ApiResponseDto<ActiveCartResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddItem(
        Guid userId,
        [FromBody] AddCartItemRequestDto request,
        CancellationToken cancellationToken)
    {
        var cart = await _cartService.AddItemAsync(userId, request, cancellationToken);

        return Ok(new ApiResponseDto<ActiveCartResponseDto>
        {
            Success = true,
            Message = "Cart item added successfully.",
            Data = cart
        });
    }

    [HttpPatch("items/{cartItemId:guid}")]
    [ProducesResponseType(typeof(ApiResponseDto<ActiveCartResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateItemQuantity(
        Guid userId,
        Guid cartItemId,
        [FromBody] UpdateCartItemQuantityRequestDto request,
        CancellationToken cancellationToken)
    {
        var cart = await _cartService.UpdateItemQuantityAsync(userId, cartItemId, request, cancellationToken);

        return Ok(new ApiResponseDto<ActiveCartResponseDto>
        {
            Success = true,
            Message = "Cart item quantity updated successfully.",
            Data = cart
        });
    }

    [HttpDelete("items/{cartItemId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveItem(
        Guid userId,
        Guid cartItemId,
        CancellationToken cancellationToken)
    {
        await _cartService.RemoveItemAsync(userId, cartItemId, cancellationToken);
        return NoContent();
    }
}
