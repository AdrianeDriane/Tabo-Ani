using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaboAni.Api.Api.Authorization;
using TaboAni.Api.Application.Configuration;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Guards;
using TaboAni.Api.Application.Interfaces.Service;

namespace TaboAni.Api.Controllers.V1;

[ApiController]
[RequireRoles(RoleCodes.Buyer)]
[Route("api/v1/orders")]
public sealed class OrdersController(
    IOrderService orderService,
    AuthOwnershipGuard authOwnershipGuard) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;
    private readonly AuthOwnershipGuard _authOwnershipGuard = authOwnershipGuard;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateOrder(
        [FromBody] OrderRequestDto orderRequestDto,
        CancellationToken cancellationToken)
    {
        if (orderRequestDto.BuyerUserId == Guid.Empty)
        {
            return BadRequest(new ErrorResponseDto
            {
                Success = false,
                Message = "Order creation failed.",
                Errors = ["Buyer user ID is required."]
            });
        }

        _authOwnershipGuard.EnsureCurrentUserMatches(orderRequestDto.BuyerUserId);
        var createdOrder = await _orderService.CreateOrderAsync(orderRequestDto, cancellationToken);

        return CreatedAtAction(
            nameof(GetOrdersByUserId),
            new { userId = createdOrder.BuyerUserId },
            new ApiResponseDto<OrderResponseDto>
            {
                Success = true,
                Message = "Order created successfully.",
                Data = createdOrder
            });
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(ApiResponseDto<IEnumerable<OrderResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrdersByUserId(Guid userId, CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
        {
            return BadRequest(new ErrorResponseDto
            {
                Success = false,
                Message = "Order lookup failed.",
                Errors = ["User ID is required."]
            });
        }

        _authOwnershipGuard.EnsureCurrentUserMatches(userId);
        var orders = await _orderService.GetOrdersByUserIdAsync(userId, cancellationToken);

        return Ok(new ApiResponseDto<IEnumerable<OrderResponseDto>>
        {
            Success = true,
            Message = "Orders retrieved successfully.",
            Data = orders
        });
    }
}
